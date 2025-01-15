using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D98 RID: 3480
	internal class CdpaSignalDimension : CdpaDimension
	{
		// Token: 0x06005EB6 RID: 24246 RVA: 0x0014781C File Offset: 0x00145A1C
		public CdpaSignalDimension(CdpaCube cube, RecordValue signal)
		{
			this.cube = cube;
			this.signalName = signal["name"].AsString;
			this.qualifiedName = QualifiedName.New(this.signalName);
			this.attributes = new Dictionary<QualifiedName, CdpaDimensionAttribute>();
			this.PopulateDimension(signal);
		}

		// Token: 0x17001BE4 RID: 7140
		// (get) Token: 0x06005EB7 RID: 24247 RVA: 0x0014786F File Offset: 0x00145A6F
		public override CdpaCube Cube
		{
			get
			{
				return this.cube;
			}
		}

		// Token: 0x17001BE5 RID: 7141
		// (get) Token: 0x06005EB8 RID: 24248 RVA: 0x00147877 File Offset: 0x00145A77
		public override QualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x17001BE6 RID: 7142
		// (get) Token: 0x06005EB9 RID: 24249 RVA: 0x0014787F File Offset: 0x00145A7F
		public override string Caption
		{
			get
			{
				return this.signalName;
			}
		}

		// Token: 0x17001BE7 RID: 7143
		// (get) Token: 0x06005EBA RID: 24250 RVA: 0x0014787F File Offset: 0x00145A7F
		public string SignalName
		{
			get
			{
				return this.signalName;
			}
		}

		// Token: 0x17001BE8 RID: 7144
		// (get) Token: 0x06005EBB RID: 24251 RVA: 0x00147887 File Offset: 0x00145A87
		public override IDictionary<QualifiedName, CdpaDimensionAttribute> Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x06005EBC RID: 24252 RVA: 0x0014788F File Offset: 0x00145A8F
		public override int GetHashCode()
		{
			return this.QualifiedName.GetHashCode();
		}

		// Token: 0x06005EBD RID: 24253 RVA: 0x0014789C File Offset: 0x00145A9C
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaSignalDimension);
		}

		// Token: 0x06005EBE RID: 24254 RVA: 0x001478AA File Offset: 0x00145AAA
		public bool Equals(CdpaSignalDimension other)
		{
			return other != null && this.cube.Service.Tenant == other.cube.Service.Tenant && this.QualifiedName.Equals(other.QualifiedName);
		}

		// Token: 0x06005EBF RID: 24255 RVA: 0x001478E9 File Offset: 0x00145AE9
		public override string ToString()
		{
			string text = "dimension(";
			QualifiedName qualifiedName = this.QualifiedName;
			return text + ((qualifiedName != null) ? qualifiedName.ToString() : null) + ")";
		}

		// Token: 0x06005EC0 RID: 24256 RVA: 0x0014790C File Offset: 0x00145B0C
		private void PopulateDimension(RecordValue signal)
		{
			foreach (IValueReference valueReference in signal["columns"].AsList)
			{
				RecordValue asRecord = valueReference.Value.AsRecord;
				CdpaDimensionAttribute cdpaDimensionAttribute = new CdpaSignalDimensionAttribute(this, asRecord);
				this.attributes.Add(cdpaDimensionAttribute.QualifiedName, cdpaDimensionAttribute);
			}
		}

		// Token: 0x04003405 RID: 13317
		private readonly CdpaCube cube;

		// Token: 0x04003406 RID: 13318
		private readonly QualifiedName qualifiedName;

		// Token: 0x04003407 RID: 13319
		private readonly string signalName;

		// Token: 0x04003408 RID: 13320
		private readonly Dictionary<QualifiedName, CdpaDimensionAttribute> attributes;
	}
}
