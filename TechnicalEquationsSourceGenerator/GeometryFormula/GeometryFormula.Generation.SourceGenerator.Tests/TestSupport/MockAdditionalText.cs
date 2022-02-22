
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System;
using System.Threading;

namespace GeometryFormula.Generation.SourceGenerator.Tests.TestSupport
{
	public class MockAdditionalText : AdditionalText
	{
		private readonly string _path;
		private readonly string _content;

		public MockAdditionalText(string path, string content)
		{
			_path = path;
			_content = content;
		}
		public override string Path => _path;

		public override SourceText? GetText(CancellationToken cancellationToken = default)
		{
			return SourceText.From(_content);
		}
	}
}
