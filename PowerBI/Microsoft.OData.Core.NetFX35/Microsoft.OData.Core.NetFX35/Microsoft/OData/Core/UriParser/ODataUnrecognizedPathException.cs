using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001F0 RID: 496
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class ODataUnrecognizedPathException : ODataException
	{
		// Token: 0x06001213 RID: 4627 RVA: 0x000419A7 File Offset: 0x0003FBA7
		public ODataUnrecognizedPathException()
			: this(Strings.ODataUriParserException_GeneralError, null)
		{
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x000419B5 File Offset: 0x0003FBB5
		public ODataUnrecognizedPathException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000419BF File Offset: 0x0003FBBF
		public ODataUnrecognizedPathException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000419C9 File Offset: 0x0003FBC9
		[SuppressMessage("Microsoft.Design", "CA1032", Justification = "Follows serialization info pattern.")]
		[SuppressMessage("Microsoft.Design", "CA1047", Justification = "Follows serialization info pattern.")]
		private ODataUnrecognizedPathException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x000419D3 File Offset: 0x0003FBD3
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x000419DB File Offset: 0x0003FBDB
		public IEnumerable<ODataPathSegment> ParsedSegments { get; set; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x000419E4 File Offset: 0x0003FBE4
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x000419EC File Offset: 0x0003FBEC
		public string CurrentSegment { get; set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x000419F5 File Offset: 0x0003FBF5
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x000419FD File Offset: 0x0003FBFD
		public IEnumerable<string> UnparsedSegments { get; set; }
	}
}
