// Copyright (c) 2020-2023 Akamitori Co., Ltd.
// This software is released under the MIT License.

using System.IO;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Verify = Microsoft.CodeAnalysis.CSharp.Testing.NUnit.CodeFixVerifier<
    test.test,
    test.testCodeFixProvider>;

namespace test.Test;

[TestFixture]
public class testCodeFixProviderTest
{
    /// <summary>
    /// Test codefix provider for lowercase type name make uppercase
    /// <seealso href="https://github.com/dotnet/roslyn-sdk/blob/main/src/Microsoft.CodeAnalysis.Testing/README.md"/>
    /// </summary>
    [Test]
    public async Task TypeNameContainingLowercase_CodeFixed()
    {
        var source = ReadCodes("TypeNameContainingLowercase.cs");
        var fixedSource = ReadCodes("TypeNameContainingLowercaseFixed.cs");
        var expected = Verify.Diagnostic()
            .WithSpan(13, 11, 13, 19)
            .WithArguments("TypeName");

        await Verify.VerifyCodeFixAsync(source, expected, fixedSource);
    }

    private static string ReadCodes(params string[] sources)
    {
        const string Path = "../../../TestData";
        var stringBuilder = new StringBuilder();
        foreach (var file in sources)
        {
            stringBuilder.Append(File.ReadAllText($"{Path}/{file}", Encoding.UTF8));
        }

        return stringBuilder.ToString();
    }
}
