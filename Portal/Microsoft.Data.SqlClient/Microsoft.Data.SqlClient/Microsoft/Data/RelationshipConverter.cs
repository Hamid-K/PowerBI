using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace Microsoft.Data
{
	// Token: 0x0200000F RID: 15
	internal sealed class RelationshipConverter : ExpandableObjectConverter
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}
	}
}
