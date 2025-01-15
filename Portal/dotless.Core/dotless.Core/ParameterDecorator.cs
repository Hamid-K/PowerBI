using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotless.Core.Exceptions;
using dotless.Core.Parameters;
using dotless.Core.Parser;
using dotless.Core.Parser.Infrastructure;

namespace dotless.Core
{
	// Token: 0x02000008 RID: 8
	public class ParameterDecorator : ILessEngine
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002895 File Offset: 0x00000A95
		public ParameterDecorator(ILessEngine underlying, IParameterSource parameterSource)
		{
			this.Underlying = underlying;
			this.parameterSource = parameterSource;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000028AC File Offset: 0x00000AAC
		public string TransformToCss(string source, string fileName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IEnumerable<KeyValuePair<string, string>> enumerable = this.parameterSource.GetParameters().Where(new Func<KeyValuePair<string, string>, bool>(ParameterDecorator.ValueIsNotNullOrEmpty));
			Parser parser = new Parser();
			stringBuilder.Append(source);
			foreach (KeyValuePair<string, string> keyValuePair in enumerable)
			{
				stringBuilder.AppendLine();
				string text = string.Format("@{0}: {1};", keyValuePair.Key, keyValuePair.Value);
				try
				{
					parser.Parse(text, "").ToCSS(new Env());
					stringBuilder.Append(text);
				}
				catch (ParserException)
				{
					stringBuilder.AppendFormat("/* Omitting variable '{0}'. The expression '{1}' is not valid. */", keyValuePair.Key, keyValuePair.Value);
				}
			}
			return this.Underlying.TransformToCss(stringBuilder.ToString(), fileName);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000299C File Offset: 0x00000B9C
		public IEnumerable<string> GetImports()
		{
			return this.Underlying.GetImports();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000029A9 File Offset: 0x00000BA9
		public void ResetImports()
		{
			this.Underlying.ResetImports();
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000029B6 File Offset: 0x00000BB6
		public bool LastTransformationSuccessful
		{
			get
			{
				return this.Underlying.LastTransformationSuccessful;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000029C3 File Offset: 0x00000BC3
		private static bool ValueIsNotNullOrEmpty(KeyValuePair<string, string> kvp)
		{
			return !string.IsNullOrEmpty(kvp.Value);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000029D4 File Offset: 0x00000BD4
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000029E1 File Offset: 0x00000BE1
		public string CurrentDirectory
		{
			get
			{
				return this.Underlying.CurrentDirectory;
			}
			set
			{
				this.Underlying.CurrentDirectory = value;
			}
		}

		// Token: 0x04000012 RID: 18
		public readonly ILessEngine Underlying;

		// Token: 0x04000013 RID: 19
		private readonly IParameterSource parameterSource;
	}
}
