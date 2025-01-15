using System;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008C7 RID: 2247
	public struct ASTSerializationSettings
	{
		// Token: 0x0600304A RID: 12362 RVA: 0x0008E657 File Offset: 0x0008C857
		private ASTSerializationSettings(ASTSerializationSettings.Options options)
		{
			this._options = options;
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x0600304B RID: 12363 RVA: 0x0008E660 File Offset: 0x0008C860
		public static ASTSerializationSettings Xml
		{
			get
			{
				return new ASTSerializationSettings((ASTSerializationSettings.Options)0);
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x0600304C RID: 12364 RVA: 0x0008E668 File Offset: 0x0008C868
		public static ASTSerializationSettings HumanReadable
		{
			get
			{
				return new ASTSerializationSettings(ASTSerializationSettings.Options.HumanReadable);
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x0600304D RID: 12365 RVA: 0x0008E670 File Offset: 0x0008C870
		public ASTSerializationSettings WithIndent
		{
			get
			{
				return new ASTSerializationSettings(this._options | ASTSerializationSettings.Options.Indent);
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x0008E67F File Offset: 0x0008C87F
		public ASTSerializationSettings WithIds
		{
			get
			{
				return new ASTSerializationSettings(this._options | ASTSerializationSettings.Options.Ids);
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x0008E68E File Offset: 0x0008C88E
		public ASTSerializationSettings WithOmitLiterals
		{
			get
			{
				return new ASTSerializationSettings(this._options | ASTSerializationSettings.Options.OmitLiterals);
			}
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x0008E69D File Offset: 0x0008C89D
		private bool HasOption(ASTSerializationSettings.Options option)
		{
			return (this._options & option) > (ASTSerializationSettings.Options)0;
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06003051 RID: 12369 RVA: 0x0008E6AA File Offset: 0x0008C8AA
		public bool HasXml
		{
			get
			{
				return !this.HasHumanReadable;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06003052 RID: 12370 RVA: 0x0008E6B5 File Offset: 0x0008C8B5
		public bool HasHumanReadable
		{
			get
			{
				return this.HasOption(ASTSerializationSettings.Options.HumanReadable);
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06003053 RID: 12371 RVA: 0x0008E6BE File Offset: 0x0008C8BE
		public bool HasIndent
		{
			get
			{
				return this.HasOption(ASTSerializationSettings.Options.Indent);
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x0008E6C7 File Offset: 0x0008C8C7
		public uint IndentIncrement
		{
			get
			{
				return (this.HasIndent > false) ? 1U : 0U;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06003055 RID: 12373 RVA: 0x0008E6D2 File Offset: 0x0008C8D2
		public bool HasIds
		{
			get
			{
				return this.HasOption(ASTSerializationSettings.Options.Ids);
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x0008E6DB File Offset: 0x0008C8DB
		public bool HasOmitLiterals
		{
			get
			{
				return this.HasOption(ASTSerializationSettings.Options.OmitLiterals);
			}
		}

		// Token: 0x0400184F RID: 6223
		private readonly ASTSerializationSettings.Options _options;

		// Token: 0x020008C8 RID: 2248
		[Flags]
		private enum Options
		{
			// Token: 0x04001851 RID: 6225
			HumanReadable = 1,
			// Token: 0x04001852 RID: 6226
			Indent = 2,
			// Token: 0x04001853 RID: 6227
			Ids = 4,
			// Token: 0x04001854 RID: 6228
			OmitLiterals = 8
		}
	}
}
