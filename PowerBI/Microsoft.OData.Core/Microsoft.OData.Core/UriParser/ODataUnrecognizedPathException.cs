using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200014B RID: 331
	[DebuggerDisplay("{Message}")]
	public sealed class ODataUnrecognizedPathException : ODataException
	{
		// Token: 0x06001113 RID: 4371 RVA: 0x000306B8 File Offset: 0x0002E8B8
		public ODataUnrecognizedPathException()
			: this(Strings.ODataUriParserException_GeneralError, null)
		{
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000306C6 File Offset: 0x0002E8C6
		public ODataUnrecognizedPathException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public ODataUnrecognizedPathException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x000306D0 File Offset: 0x0002E8D0
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x000306D8 File Offset: 0x0002E8D8
		public IEnumerable<ODataPathSegment> ParsedSegments { get; set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x000306E1 File Offset: 0x0002E8E1
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x000306E9 File Offset: 0x0002E8E9
		public string CurrentSegment { get; set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x000306F2 File Offset: 0x0002E8F2
		// (set) Token: 0x0600111B RID: 4379 RVA: 0x000306FA File Offset: 0x0002E8FA
		public IEnumerable<string> UnparsedSegments { get; set; }
	}
}
