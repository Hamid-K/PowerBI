using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000BA RID: 186
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class ExceptionDetails : ISerializableWithWriter
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x00016A08 File Offset: 0x00014C08
		internal static ExceptionDetails CreateWithoutStackInfo(Exception exception, ExceptionDetails parentExceptionDetails)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			ExceptionDetails exceptionDetails = new ExceptionDetails
			{
				id = exception.GetHashCode(),
				typeName = exception.GetType().FullName,
				message = exception.Message
			};
			if (parentExceptionDetails != null)
			{
				exceptionDetails.outerId = parentExceptionDetails.id;
			}
			return exceptionDetails;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00016A64 File Offset: 0x00014C64
		public void Serialize(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty("id", new int?(this.id));
			serializationWriter.WriteProperty("outerId", new int?(this.outerId));
			serializationWriter.WriteProperty("typeName", this.typeName);
			serializationWriter.WriteProperty("message", this.message);
			serializationWriter.WriteProperty("hasFullStack", new bool?(this.hasFullStack));
			serializationWriter.WriteProperty("stack", this.stack);
			serializationWriter.WriteProperty("parsedStack", this.parsedStack.ToList<ISerializableWithWriter>());
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00016AFC File Offset: 0x00014CFC
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00016B04 File Offset: 0x00014D04
		public int id { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00016B0D File Offset: 0x00014D0D
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00016B15 File Offset: 0x00014D15
		public int outerId { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00016B1E File Offset: 0x00014D1E
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x00016B26 File Offset: 0x00014D26
		public string typeName { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00016B2F File Offset: 0x00014D2F
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x00016B37 File Offset: 0x00014D37
		public string message { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00016B40 File Offset: 0x00014D40
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x00016B48 File Offset: 0x00014D48
		public bool hasFullStack { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00016B51 File Offset: 0x00014D51
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x00016B59 File Offset: 0x00014D59
		public string stack { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00016B62 File Offset: 0x00014D62
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x00016B6A File Offset: 0x00014D6A
		public IList<StackFrame> parsedStack { get; set; }

		// Token: 0x060005F6 RID: 1526 RVA: 0x00016B73 File Offset: 0x00014D73
		public ExceptionDetails()
			: this("AI.ExceptionDetails", "ExceptionDetails")
		{
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00016B85 File Offset: 0x00014D85
		protected ExceptionDetails(string fullName, string name)
		{
			this.typeName = "";
			this.message = "";
			this.hasFullStack = true;
			this.stack = "";
			this.parsedStack = new List<StackFrame>();
		}
	}
}
