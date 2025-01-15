using System;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x02000018 RID: 24
	internal sealed class JsonWriterSettings
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003DC0 File Offset: 0x00001FC0
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003DC8 File Offset: 0x00001FC8
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
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003DD1 File Offset: 0x00001FD1
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00003DD9 File Offset: 0x00001FD9
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
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003DE2 File Offset: 0x00001FE2
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00003DEA File Offset: 0x00001FEA
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
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003DF3 File Offset: 0x00001FF3
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003DFB File Offset: 0x00001FFB
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
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003E04 File Offset: 0x00002004
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003E0C File Offset: 0x0000200C
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
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003E15 File Offset: 0x00002015
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
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003E2C File Offset: 0x0000202C
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003E34 File Offset: 0x00002034
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
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003E3D File Offset: 0x0000203D
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003E45 File Offset: 0x00002045
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

		// Token: 0x060000D0 RID: 208 RVA: 0x00003E4E File Offset: 0x0000204E
		internal JsonWriterSettings()
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003E56 File Offset: 0x00002056
		internal JsonWriterSettings(bool indent, bool newLineOnArray, bool newLineOnArrayPrimitive, bool newLineOnObject, bool newLineOnProperty)
			: this(indent, null, newLineOnArray, newLineOnArrayPrimitive, newLineOnObject, newLineOnProperty, false)
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003E67 File Offset: 0x00002067
		internal JsonWriterSettings(bool indent, string indentString, bool newLineOnArray, bool newLineOnArrayPrimitive, bool newLineOnObject, bool newLineOnProperty, bool startingBraceOnParentLine = false)
		{
			this.enableIdentation = indent;
			this.newLineOnArray = newLineOnArray;
			this.newLineOnArrayPrimitive = newLineOnArrayPrimitive;
			this.newLineOnObject = newLineOnObject;
			this.newLineOnProperty = newLineOnProperty;
			this.indentationString = indentString;
			this.startingBraceOnParentLine = startingBraceOnParentLine;
		}

		// Token: 0x040000A2 RID: 162
		private bool enableIdentation;

		// Token: 0x040000A3 RID: 163
		private string indentationString;

		// Token: 0x040000A4 RID: 164
		private bool newLineOnProperty;

		// Token: 0x040000A5 RID: 165
		private bool newLineOnObject;

		// Token: 0x040000A6 RID: 166
		private bool newLineOnArray;

		// Token: 0x040000A7 RID: 167
		private bool newLineOnArrayPrimitive;

		// Token: 0x040000A8 RID: 168
		private bool startingBraceOnParentLine;
	}
}
