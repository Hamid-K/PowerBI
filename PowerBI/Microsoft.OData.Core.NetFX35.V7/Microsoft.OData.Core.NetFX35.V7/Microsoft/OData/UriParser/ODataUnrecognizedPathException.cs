using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000107 RID: 263
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class ODataUnrecognizedPathException : ODataException
	{
		// Token: 0x06000C5E RID: 3166 RVA: 0x000222EC File Offset: 0x000204EC
		public ODataUnrecognizedPathException()
			: this(Strings.ODataUriParserException_GeneralError, null)
		{
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x000222FA File Offset: 0x000204FA
		public ODataUnrecognizedPathException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00009008 File Offset: 0x00007208
		public ODataUnrecognizedPathException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00009012 File Offset: 0x00007212
		[SuppressMessage("Microsoft.Design", "CA1047", Justification = "Follows serialization info pattern.")]
		[SuppressMessage("Microsoft.Design", "CA1032", Justification = "Follows serialization info pattern.")]
		private ODataUnrecognizedPathException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00022304 File Offset: 0x00020504
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x0002230C File Offset: 0x0002050C
		public IEnumerable<ODataPathSegment> ParsedSegments { get; set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00022315 File Offset: 0x00020515
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x0002231D File Offset: 0x0002051D
		public string CurrentSegment { get; set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00022326 File Offset: 0x00020526
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x0002232E File Offset: 0x0002052E
		public IEnumerable<string> UnparsedSegments { get; set; }
	}
}
