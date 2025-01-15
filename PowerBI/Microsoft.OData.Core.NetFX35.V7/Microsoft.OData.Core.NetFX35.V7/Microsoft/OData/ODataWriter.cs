using System;

namespace Microsoft.OData
{
	// Token: 0x020000A4 RID: 164
	public abstract class ODataWriter
	{
		// Token: 0x06000619 RID: 1561
		public abstract void WriteStart(ODataResourceSet resourceSet);

		// Token: 0x0600061A RID: 1562 RVA: 0x0001069A File Offset: 0x0000E89A
		public ODataWriter Write(ODataResourceSet resourceSet)
		{
			this.WriteStart(resourceSet);
			this.WriteEnd();
			return this;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000106AA File Offset: 0x0000E8AA
		public ODataWriter Write(ODataResourceSet resourceSet, Action nestedAction)
		{
			this.WriteStart(resourceSet);
			nestedAction.Invoke();
			this.WriteEnd();
			return this;
		}

		// Token: 0x0600061C RID: 1564
		public abstract void WriteStart(ODataResource resource);

		// Token: 0x0600061D RID: 1565 RVA: 0x000106C0 File Offset: 0x0000E8C0
		public ODataWriter Write(ODataResource resource)
		{
			this.WriteStart(resource);
			this.WriteEnd();
			return this;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000106D0 File Offset: 0x0000E8D0
		public ODataWriter Write(ODataResource resource, Action nestedAction)
		{
			this.WriteStart(resource);
			nestedAction.Invoke();
			this.WriteEnd();
			return this;
		}

		// Token: 0x0600061F RID: 1567
		public abstract void WriteStart(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x06000620 RID: 1568 RVA: 0x000106E6 File Offset: 0x0000E8E6
		public ODataWriter Write(ODataNestedResourceInfo nestedResourceInfo)
		{
			this.WriteStart(nestedResourceInfo);
			this.WriteEnd();
			return this;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000106F6 File Offset: 0x0000E8F6
		public ODataWriter Write(ODataNestedResourceInfo nestedResourceInfo, Action nestedAction)
		{
			this.WriteStart(nestedResourceInfo);
			nestedAction.Invoke();
			this.WriteEnd();
			return this;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void WritePrimitive(ODataPrimitiveValue primitiveValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001070C File Offset: 0x0000E90C
		public ODataWriter Write(ODataPrimitiveValue primitiveValue)
		{
			this.WritePrimitive(primitiveValue);
			return this;
		}

		// Token: 0x06000624 RID: 1572
		public abstract void WriteEnd();

		// Token: 0x06000625 RID: 1573
		public abstract void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x06000626 RID: 1574
		public abstract void Flush();
	}
}
