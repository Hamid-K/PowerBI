using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000030 RID: 48
	internal class MatrixRow2005 : ReportObject
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00003843 File Offset: 0x00001A43
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00003851 File Offset: 0x00001A51
		public ReportSize Height
		{
			get
			{
				return base.PropertyStore.GetSize(0);
			}
			set
			{
				base.PropertyStore.SetSize(0, value);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00003860 File Offset: 0x00001A60
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00003873 File Offset: 0x00001A73
		[XmlElement(typeof(RdlCollection<MatrixCell2005>))]
		[XmlArrayItem("MatrixCell", typeof(MatrixCell2005))]
		public IList<MatrixCell2005> MatrixCells
		{
			get
			{
				return (IList<MatrixCell2005>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00003882 File Offset: 0x00001A82
		public MatrixRow2005()
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000388A File Offset: 0x00001A8A
		public MatrixRow2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00003893 File Offset: 0x00001A93
		public override void Initialize()
		{
			base.Initialize();
			this.Height = Constants.DefaultZeroSize;
			this.MatrixCells = new RdlCollection<MatrixCell2005>();
		}

		// Token: 0x0200030A RID: 778
		internal class Definition : DefinitionStore<MatrixRow2005, MatrixRow2005.Definition.Properties>
		{
			// Token: 0x06001706 RID: 5894 RVA: 0x000364CA File Offset: 0x000346CA
			private Definition()
			{
			}

			// Token: 0x0200043E RID: 1086
			public enum Properties
			{
				// Token: 0x0400089E RID: 2206
				Height,
				// Token: 0x0400089F RID: 2207
				MatrixCells
			}
		}
	}
}
