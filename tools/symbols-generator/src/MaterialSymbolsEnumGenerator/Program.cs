using System.CodeDom;
using System.CodeDom.Compiler;
using System.Globalization;
using MaterialSymbolsEnumGenerator;
using Microsoft.CSharp;

const string outputFilename = "Symbol.cs";

Console.WriteLine("Downloading Material Symbols codepoints...");
using var client = new HttpClient();
var codepoints = await client.GetStringAsync(Constants.CodepointsSourceUrl);

Console.WriteLine($"Generating enum in {outputFilename}...");

var codepointsArray = codepoints.Split('\n', StringSplitOptions.RemoveEmptyEntries);
var code = GenerateEnum(codepointsArray);

WriteCodeToFile(code, outputFilename);

Console.WriteLine($"Enum generated successfully in {outputFilename}");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
return;

static CodeCompileUnit GenerateEnum(string[] codepoints)
{
    var unit = new CodeCompileUnit();
    var @namespace = new CodeNamespace("WPF.Material.Components");
    
    @namespace.Comments.Add(new CodeCommentStatement($"Generated at: {DateTimeOffset.UtcNow}"));
    
    unit.Namespaces.Add(@namespace);

    var @enum = new CodeTypeDeclaration("Symbol")
    {
        IsEnum = true,
        TypeAttributes = System.Reflection.TypeAttributes.Public,
        Comments =
        {
            new CodeCommentStatement("<summary>", true),
            new CodeCommentStatement("Defines the symbols contained in Material Symbols font.", true),
            new CodeCommentStatement("</summary>", true)
        }
    };

    foreach (var codepoint in codepoints)
    {
        var parts = codepoint.Split(' ');
        if (parts.Length is not 2)
        {
            throw new FormatException("Expected line format: <name> <code>");
        }

        var name = parts[0];
        var code = int.Parse(parts[1], NumberStyles.HexNumber);

        @enum.Members.Add(new CodeMemberField(typeof(int), name.ToMemberName())
        {
            InitExpression = new CodePrimitiveExpression(code),
            Comments =
            {
                new CodeCommentStatement("<summary>", true),
                new CodeCommentStatement($"Identifies the '{name}' symbol.", true),
                new CodeCommentStatement("</summary>", true)
            }
        });
    }

    @namespace.Types.Add(@enum);
    return unit;
}

static void WriteCodeToFile(CodeCompileUnit unit, string filename)
{
    using var provider = new CSharpCodeProvider();
    using var writer = new StreamWriter(filename);

    var options = new CodeGeneratorOptions
    {
        BracingStyle = "C",
        IndentString = "    ",
        BlankLinesBetweenMembers = false
    };

    provider.GenerateCodeFromCompileUnit(unit, writer, options);
}