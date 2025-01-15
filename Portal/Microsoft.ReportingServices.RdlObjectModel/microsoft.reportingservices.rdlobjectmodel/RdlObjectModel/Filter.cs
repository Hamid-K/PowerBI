using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000148 RID: 328
	public class Filter : ReportObject
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0001DA50 File Offset: 0x0001BC50
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x0001DA5E File Offset: 0x0001BC5E
		public ReportExpression FilterExpression
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0001DA72 File Offset: 0x0001BC72
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x0001DA80 File Offset: 0x0001BC80
		public Operators Operator
		{
			get
			{
				return (Operators)base.PropertyStore.GetInteger(1);
			}
			set
			{
				base.PropertyStore.SetInteger(1, (int)value);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0001DA8F File Offset: 0x0001BC8F
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x0001DAA2 File Offset: 0x0001BCA2
		[XmlElement(typeof(RdlCollection<ReportExpression>))]
		[XmlArrayItem("FilterValue", typeof(ReportExpression))]
		public IList<ReportExpression> FilterValues
		{
			get
			{
				return (IList<ReportExpression>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001DAB1 File Offset: 0x0001BCB1
		public Filter()
		{
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001DAB9 File Offset: 0x0001BCB9
		internal Filter(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001DAC2 File Offset: 0x0001BCC2
		public override void Initialize()
		{
			base.Initialize();
			this.FilterValues = new RdlCollection<ReportExpression>();
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001DAD5 File Offset: 0x0001BCD5
		public bool Equals(Filter filter)
		{
			return filter != null && (this.FilterExpression == filter.FilterExpression && this.FilterValues == filter.FilterValues && this.Operator == filter.Operator) && base.Equals(filter);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001DB14 File Offset: 0x0001BD14
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Filter);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001DB24 File Offset: 0x0001BD24
		public override int GetHashCode()
		{
			return this.FilterExpression.GetHashCode();
		}

		// Token: 0x02000377 RID: 887
		internal class Definition
		{
			// Token: 0x02000492 RID: 1170
			internal enum Properties
			{
				// Token: 0x04000BB8 RID: 3000
				FilterExpression,
				// Token: 0x04000BB9 RID: 3001
				Operator,
				// Token: 0x04000BBA RID: 3002
				FilterValues
			}
		}
	}
}
