using System;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x02000015 RID: 21
	internal sealed class JsonWriterSettings
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003E94 File Offset: 0x00002094
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003E9C File Offset: 0x0000209C
		public bool EnableIndentation
		{
			get
			{
				return this.enableIdentation;
			}
			set
			{
				this.enableIdentation = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003EA5 File Offset: 0x000020A5
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003EAD File Offset: 0x000020AD
		public string IndentationString
		{
			get
			{
				return this.indentationString;
			}
			set
			{
				this.indentationString = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003EB6 File Offset: 0x000020B6
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003EBE File Offset: 0x000020BE
		public bool NewLineOnProperty
		{
			get
			{
				return this.newLineOnProperty;
			}
			set
			{
				this.newLineOnProperty = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003EC7 File Offset: 0x000020C7
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00003ECF File Offset: 0x000020CF
		public bool NewLineOnObject
		{
			get
			{
				return this.newLineOnObject;
			}
			set
			{
				this.newLineOnObject = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003ED8 File Offset: 0x000020D8
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003EE0 File Offset: 0x000020E0
		public bool NewLineOnArray
		{
			get
			{
				return this.newLineOnArray;
			}
			set
			{
				this.newLineOnArray = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00003EE9 File Offset: 0x000020E9
		public bool NewLineOnScope
		{
			set
			{
				this.newLineOnArray = value;
				this.newLineOnObject = value;
				this.newLineOnProperty = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003F00 File Offset: 0x00002100
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00003F08 File Offset: 0x00002108
		public bool NewLineOnArrayPrimitive
		{
			get
			{
				return this.newLineOnArrayPrimitive;
			}
			set
			{
				this.newLineOnArrayPrimitive = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003F11 File Offset: 0x00002111
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00003F19 File Offset: 0x00002119
		public bool StartingBraceOnParentLine
		{
			get
			{
				return this.startingBraceOnParentLine;
			}
			set
			{
				this.startingBraceOnParentLine = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003F22 File Offset: 0x00002122
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003F2A File Offset: 0x0000212A
		public bool EscapeUnicode { get; set; }

		// Token: 0x060000D3 RID: 211 RVA: 0x00003F33 File Offset: 0x00002133
		internal JsonWriterSettings()
		{
			this.EscapeUnicode = true;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003F42 File Offset: 0x00002142
		internal JsonWriterSettings(bool indent, bool newLineOnArray, bool newLineOnArrayPrimitive, bool newLineOnObject, bool newLineOnProperty)
			: this(indent, null, newLineOnArray, newLineOnArrayPrimitive, newLineOnObject, newLineOnProperty, false)
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003F53 File Offset: 0x00002153
		internal JsonWriterSettings(bool indent, string indentString, bool newLineOnArray, bool newLineOnArrayPrimitive, bool newLineOnObject, bool newLineOnProperty, bool startingBraceOnParentLine = false)
			: this()
		{
			this.enableIdentation = indent;
			this.newLineOnArray = newLineOnArray;
			this.newLineOnArrayPrimitive = newLineOnArrayPrimitive;
			this.newLineOnObject = newLineOnObject;
			this.newLineOnProperty = newLineOnProperty;
			this.indentationString = indentString;
			this.startingBraceOnParentLine = startingBraceOnParentLine;
		}

		// Token: 0x0400007E RID: 126
		private bool enableIdentation;

		// Token: 0x0400007F RID: 127
		private string indentationString;

		// Token: 0x04000080 RID: 128
		private bool newLineOnProperty;

		// Token: 0x04000081 RID: 129
		private bool newLineOnObject;

		// Token: 0x04000082 RID: 130
		private bool newLineOnArray;

		// Token: 0x04000083 RID: 131
		private bool newLineOnArrayPrimitive;

		// Token: 0x04000084 RID: 132
		private bool startingBraceOnParentLine;
	}
}
