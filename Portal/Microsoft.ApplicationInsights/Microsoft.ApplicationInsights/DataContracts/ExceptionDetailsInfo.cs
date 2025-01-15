using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000CC RID: 204
	public sealed class ExceptionDetailsInfo
	{
		// Token: 0x060006F6 RID: 1782 RVA: 0x00018774 File Offset: 0x00016974
		public ExceptionDetailsInfo(int id, int outerId, string typeName, string message, bool hasFullStack, string stack, IEnumerable<StackFrame> parsedStack)
		{
			ExceptionDetails exceptionDetails = new ExceptionDetails();
			exceptionDetails.id = id;
			exceptionDetails.outerId = outerId;
			exceptionDetails.typeName = typeName;
			exceptionDetails.message = message;
			exceptionDetails.hasFullStack = hasFullStack;
			exceptionDetails.stack = stack;
			exceptionDetails.parsedStack = parsedStack.Select((StackFrame ps) => ps.Data).ToList<StackFrame>();
			this.InternalExceptionDetails = exceptionDetails;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000187F0 File Offset: 0x000169F0
		internal ExceptionDetailsInfo(ExceptionDetails exceptionDetails)
		{
			this.InternalExceptionDetails = exceptionDetails;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x000187FF File Offset: 0x000169FF
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0001880C File Offset: 0x00016A0C
		public string TypeName
		{
			get
			{
				return this.InternalExceptionDetails.typeName;
			}
			set
			{
				this.InternalExceptionDetails.typeName = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0001881A File Offset: 0x00016A1A
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00018827 File Offset: 0x00016A27
		public string Message
		{
			get
			{
				return this.InternalExceptionDetails.message;
			}
			set
			{
				this.InternalExceptionDetails.message = value;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00018835 File Offset: 0x00016A35
		internal ExceptionDetails ExceptionDetails
		{
			get
			{
				return this.InternalExceptionDetails;
			}
		}

		// Token: 0x040002C4 RID: 708
		internal readonly ExceptionDetails InternalExceptionDetails;
	}
}
