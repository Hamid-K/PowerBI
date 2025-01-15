using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200004F RID: 79
	internal class Details2005 : ReportObject
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00004A6B File Offset: 0x00002C6B
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00004A7E File Offset: 0x00002C7E
		[XmlElement(typeof(RdlCollection<TableRow2005>))]
		[XmlArrayItem("TableRow", typeof(TableRow2005))]
		public IList<TableRow2005> TableRows
		{
			get
			{
				return (IList<TableRow2005>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00004A8D File Offset: 0x00002C8D
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x00004AA0 File Offset: 0x00002CA0
		public Group Grouping
		{
			get
			{
				return (Group)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00004AAF File Offset: 0x00002CAF
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00004AC2 File Offset: 0x00002CC2
		[XmlElement(typeof(RdlCollection<SortExpression>))]
		public IList<SortExpression> Sorting
		{
			get
			{
				return (IList<SortExpression>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00004AD1 File Offset: 0x00002CD1
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public Visibility Visibility
		{
			get
			{
				return (Visibility)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00004AF3 File Offset: 0x00002CF3
		public Details2005()
		{
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00004AFB File Offset: 0x00002CFB
		public Details2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00004B04 File Offset: 0x00002D04
		public override void Initialize()
		{
			base.Initialize();
			this.TableRows = new RdlCollection<TableRow2005>();
		}

		// Token: 0x0200031D RID: 797
		internal class Definition : DefinitionStore<Details2005, Details2005.Definition.Properties>
		{
			// Token: 0x06001719 RID: 5913 RVA: 0x00036562 File Offset: 0x00034762
			private Definition()
			{
			}

			// Token: 0x02000451 RID: 1105
			public enum Properties
			{
				// Token: 0x040008FD RID: 2301
				TableRows,
				// Token: 0x040008FE RID: 2302
				Grouping,
				// Token: 0x040008FF RID: 2303
				Sorting,
				// Token: 0x04000900 RID: 2304
				Visibility
			}
		}
	}
}
