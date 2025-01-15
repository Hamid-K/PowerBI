using System;
using System.CodeDom.Compiler;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000B3 RID: 179
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class DataPoint : ISerializableWithWriter
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x00016368 File Offset: 0x00014568
		public DataPoint DeepClone()
		{
			return new DataPoint
			{
				ns = this.ns,
				name = this.name,
				kind = this.kind,
				value = this.value,
				count = this.count,
				min = this.min,
				max = this.max,
				stdDev = this.stdDev
			};
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000163DC File Offset: 0x000145DC
		public void Serialize(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty("ns", this.ns);
			serializationWriter.WriteProperty("name", this.name);
			serializationWriter.WriteProperty("kind", this.kind.ToString());
			serializationWriter.WriteProperty("value", new double?(this.value));
			serializationWriter.WriteProperty("count", (this.count != null) ? this.count : new int?(1));
			serializationWriter.WriteProperty("min", this.min);
			serializationWriter.WriteProperty("max", this.max);
			serializationWriter.WriteProperty("stdDev", this.stdDev);
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0001649C File Offset: 0x0001469C
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x000164A4 File Offset: 0x000146A4
		public string ns { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x000164AD File Offset: 0x000146AD
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x000164B5 File Offset: 0x000146B5
		public string name { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000164BE File Offset: 0x000146BE
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x000164C6 File Offset: 0x000146C6
		public DataPointType kind { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x000164CF File Offset: 0x000146CF
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x000164D7 File Offset: 0x000146D7
		public double value { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000164E0 File Offset: 0x000146E0
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x000164E8 File Offset: 0x000146E8
		public int? count { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000164F1 File Offset: 0x000146F1
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x000164F9 File Offset: 0x000146F9
		public double? min { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00016502 File Offset: 0x00014702
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0001650A File Offset: 0x0001470A
		public double? max { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00016513 File Offset: 0x00014713
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0001651B File Offset: 0x0001471B
		public double? stdDev { get; set; }

		// Token: 0x060005AE RID: 1454 RVA: 0x00016524 File Offset: 0x00014724
		public DataPoint()
			: this("AI.DataPoint", "DataPoint")
		{
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00016536 File Offset: 0x00014736
		protected DataPoint(string fullName, string name)
		{
			this.ns = "";
			this.name = "";
			this.kind = DataPointType.Measurement;
		}
	}
}
