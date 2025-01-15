using System;
using System.Reflection;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000366 RID: 870
	internal class KnownTypeParameterFactory : IParameterFactory
	{
		// Token: 0x060019F4 RID: 6644 RVA: 0x00060024 File Offset: 0x0005E224
		public bool TryCreateParameterMetadata(ParameterInfo methodParameter, out ParameterMetadata parameterMetadata)
		{
			parameterMetadata = null;
			if (!WireFieldMetadata.IsKnownType(methodParameter.ParameterType))
			{
				return false;
			}
			parameterMetadata = new ParameterMetadata(methodParameter.ParameterType, methodParameter.Name, new WireFieldMetadata[]
			{
				new WireFieldMetadata(methodParameter.ParameterType, methodParameter.Name, new AssignedValue(methodParameter.Name), ParameterFactoryUtility.GetParameterAttribute(methodParameter))
			}, new PropertyMetadata(methodParameter.ParameterType, methodParameter.Name));
			return true;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x00060093 File Offset: 0x0005E293
		public string ValidTypesAsString
		{
			get
			{
				return "primitive types and enums";
			}
		}
	}
}
