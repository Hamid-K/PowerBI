using System;
using System.CodeDom.Compiler;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000C2 RID: 194
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class StackFrame : ISerializableWithWriter
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x000175DC File Offset: 0x000157DC
		public void Serialize(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty("level", new int?(this.level));
			serializationWriter.WriteProperty("method", this.method);
			serializationWriter.WriteProperty("assembly", this.assembly);
			serializationWriter.WriteProperty("fileName", this.fileName);
			serializationWriter.WriteProperty("line", new int?(this.line));
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00017648 File Offset: 0x00015848
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x00017650 File Offset: 0x00015850
		public int level { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00017659 File Offset: 0x00015859
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x00017661 File Offset: 0x00015861
		public string method { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0001766A File Offset: 0x0001586A
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x00017672 File Offset: 0x00015872
		public string assembly { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001767B File Offset: 0x0001587B
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x00017683 File Offset: 0x00015883
		public string fileName { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0001768C File Offset: 0x0001588C
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x00017694 File Offset: 0x00015894
		public int line { get; set; }

		// Token: 0x06000665 RID: 1637 RVA: 0x0001769D File Offset: 0x0001589D
		public StackFrame()
			: this("AI.StackFrame", "StackFrame")
		{
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000176AF File Offset: 0x000158AF
		protected StackFrame(string fullName, string name)
		{
			this.method = "";
			this.assembly = "";
			this.fileName = "";
		}
	}
}
