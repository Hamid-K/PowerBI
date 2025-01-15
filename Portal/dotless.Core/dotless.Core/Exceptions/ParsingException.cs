using System;
using dotless.Core.Parser;

namespace dotless.Core.Exceptions
{
	// Token: 0x020000BE RID: 190
	public class ParsingException : Exception
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00017F1D File Offset: 0x0001611D
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00017F25 File Offset: 0x00016125
		public NodeLocation Location { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00017F2E File Offset: 0x0001612E
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x00017F36 File Offset: 0x00016136
		public NodeLocation CallLocation { get; set; }

		// Token: 0x06000576 RID: 1398 RVA: 0x00017F3F File Offset: 0x0001613F
		public ParsingException(string message, NodeLocation location)
			: this(message, null, location, null)
		{
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00017F4B File Offset: 0x0001614B
		public ParsingException(string message, NodeLocation location, NodeLocation callLocation)
			: this(message, null, location, callLocation)
		{
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00017F57 File Offset: 0x00016157
		public ParsingException(Exception innerException, NodeLocation location)
			: this(innerException, location, null)
		{
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00017F62 File Offset: 0x00016162
		public ParsingException(Exception innerException, NodeLocation location, NodeLocation callLocation)
			: this(innerException.Message, innerException, location, callLocation)
		{
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00017F73 File Offset: 0x00016173
		public ParsingException(string message, Exception innerException, NodeLocation location)
			: this(message, innerException, location, null)
		{
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00017F7F File Offset: 0x0001617F
		public ParsingException(string message, Exception innerException, NodeLocation location, NodeLocation callLocation)
			: base(message, innerException)
		{
			this.Location = location;
			this.CallLocation = callLocation;
		}
	}
}
