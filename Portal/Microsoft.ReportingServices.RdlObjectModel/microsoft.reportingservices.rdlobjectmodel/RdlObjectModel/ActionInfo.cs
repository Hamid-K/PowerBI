using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001DD RID: 477
	public class ActionInfo : ReportObject
	{
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0002602E File Offset: 0x0002422E
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x00026041 File Offset: 0x00024241
		[XmlElement(typeof(RdlCollection<Action>))]
		public IList<Action> Actions
		{
			get
			{
				return (IList<Action>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00026050 File Offset: 0x00024250
		public ActionInfo()
		{
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00026058 File Offset: 0x00024258
		internal ActionInfo(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00026061 File Offset: 0x00024261
		public override void Initialize()
		{
			base.Initialize();
			this.Actions = new RdlCollection<Action>();
		}

		// Token: 0x020003EB RID: 1003
		internal class Definition : DefinitionStore<ActionInfo, ActionInfo.Definition.Properties>
		{
			// Token: 0x060018AD RID: 6317 RVA: 0x0003BB27 File Offset: 0x00039D27
			private Definition()
			{
			}

			// Token: 0x020004FD RID: 1277
			internal enum Properties
			{
				// Token: 0x0400109E RID: 4254
				Actions,
				// Token: 0x0400109F RID: 4255
				LayoutDirection,
				// Token: 0x040010A0 RID: 4256
				Style
			}
		}
	}
}
