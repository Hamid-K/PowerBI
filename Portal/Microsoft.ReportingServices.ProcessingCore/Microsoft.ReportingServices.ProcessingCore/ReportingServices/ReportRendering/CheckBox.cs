using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200002D RID: 45
	internal sealed class CheckBox : ReportItem
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x0000E006 File Offset: 0x0000C206
		internal CheckBox(string uniqueName, int intUniqueName, CheckBox reportItemDef, CheckBoxInstance reportItemInstance, RenderingContext renderingContext)
			: base(uniqueName, intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000E018 File Offset: 0x0000C218
		public bool Value
		{
			get
			{
				ExpressionInfo value = ((CheckBox)base.ReportItemDef).Value;
				if (value.Type == ExpressionInfo.Types.Constant)
				{
					return value.BoolValue;
				}
				return base.ReportItemInstance != null && ((CheckBoxInstanceInfo)base.InstanceInfo).Value;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000E060 File Offset: 0x0000C260
		public bool HideDuplicates
		{
			get
			{
				return ((CheckBox)base.ReportItemDef).HideDuplicates != null;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000E075 File Offset: 0x0000C275
		public bool Duplicate
		{
			get
			{
				return base.ReportItemInstance != null && ((CheckBoxInstanceInfo)base.InstanceInfo).Duplicate;
			}
		}
	}
}
