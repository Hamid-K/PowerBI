using System;

namespace Microsoft.DataShaping.Common.Json
{
	// Token: 0x02000021 RID: 33
	internal sealed class JsonWriterSettings
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00004A18 File Offset: 0x00002C18
		internal JsonWriterSettings()
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004A20 File Offset: 0x00002C20
		internal JsonWriterSettings(bool indent, bool newLineOnArray, bool newLineOnArrayPrimitive, bool newLineOnObject, bool newLineOnProperty)
			: this(indent, null, newLineOnArray, newLineOnArrayPrimitive, newLineOnObject, newLineOnProperty, false)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004A31 File Offset: 0x00002C31
		internal JsonWriterSettings(bool indent, string indentString, bool newLineOnArray, bool newLineOnArrayPrimitive, bool newLineOnObject, bool newLineOnProperty, bool startingBraceOnParentLine = false)
		{
			this.EnableIndentation = indent;
			this.NewLineOnArray = newLineOnArray;
			this.NewLineOnArrayPrimitive = newLineOnArrayPrimitive;
			this.NewLineOnObject = newLineOnObject;
			this.NewLineOnProperty = newLineOnProperty;
			this.IndentationString = indentString;
			this.StartingBraceOnParentLine = startingBraceOnParentLine;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00004A6E File Offset: 0x00002C6E
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00004A76 File Offset: 0x00002C76
		public bool EnableIndentation { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004A7F File Offset: 0x00002C7F
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00004A87 File Offset: 0x00002C87
		public string IndentationString { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004A90 File Offset: 0x00002C90
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00004A98 File Offset: 0x00002C98
		public bool NewLineOnProperty { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004AA1 File Offset: 0x00002CA1
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00004AA9 File Offset: 0x00002CA9
		public bool NewLineOnObject { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004AB2 File Offset: 0x00002CB2
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00004ABA File Offset: 0x00002CBA
		public bool NewLineOnArray { get; set; }

		// Token: 0x1700000E RID: 14
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00004AC3 File Offset: 0x00002CC3
		public bool NewLineOnScope
		{
			set
			{
				this.NewLineOnArray = value;
				this.NewLineOnObject = value;
				this.NewLineOnProperty = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004ADA File Offset: 0x00002CDA
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00004AE2 File Offset: 0x00002CE2
		public bool NewLineOnArrayPrimitive { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00004AEB File Offset: 0x00002CEB
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00004AF3 File Offset: 0x00002CF3
		public bool StartingBraceOnParentLine { get; set; }
	}
}
