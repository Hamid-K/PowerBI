using System;
using System.IO;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000072 RID: 114
	public class DataUriFunction : Function
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x00015B60 File Offset: 0x00013D60
		protected override Node Evaluate(Env env)
		{
			string dataUriFilename = this.GetDataUriFilename();
			string text = this.ConvertFileToBase64(dataUriFilename);
			string mimeType = this.GetMimeType(dataUriFilename);
			return new TextNode(string.Format("url(\"data:{0};base64,{1}\")", mimeType, text));
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00015B98 File Offset: 0x00013D98
		private string GetDataUriFilename()
		{
			Guard.ExpectMinArguments(1, base.Arguments.Count, this, base.Location);
			Node node = base.Arguments[0];
			if (base.Arguments.Count > 1)
			{
				node = base.Arguments[1];
			}
			Guard.ExpectNode<Quoted>(node, this, base.Location);
			string value = ((Quoted)node).Value;
			Guard.Expect(!value.StartsWith("http://") && !value.StartsWith("https://"), string.Format("Invalid filename passed to data-uri '{0}'. Filename must be a local file", value), base.Location);
			return value;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00015C34 File Offset: 0x00013E34
		private string ConvertFileToBase64(string filename)
		{
			string text;
			try
			{
				text = Convert.ToBase64String(File.ReadAllBytes(filename));
			}
			catch (IOException ex)
			{
				throw new ParsingException(string.Format("Data-uri function could not read file '{0}'", filename), ex, base.Location);
			}
			return text;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00015C7C File Offset: 0x00013E7C
		private string GetMimeType(string filename)
		{
			if (base.Arguments.Count > 1)
			{
				Guard.ExpectNode<Quoted>(base.Arguments[0], this, base.Location);
				string text = ((Quoted)base.Arguments[0]).Value;
				if (text.IndexOf(';') > -1)
				{
					text = text.Split(new char[] { ';' })[0];
				}
				return text;
			}
			return new MimeTypeLookup().ByFilename(filename);
		}
	}
}
