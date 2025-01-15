using System;
using System.Reflection;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000368 RID: 872
	internal class PrivateInformationParameterFactory : IParameterFactory
	{
		// Token: 0x060019FB RID: 6651 RVA: 0x00060224 File Offset: 0x0005E424
		public bool TryCreateParameterMetadata(ParameterInfo methodParameter, out ParameterMetadata parameterMetadata)
		{
			parameterMetadata = null;
			Type type = typeof(IContainsPrivateInformation);
			if (methodParameter.ParameterType.Assembly.FullName == type.Assembly.FullName)
			{
				type = methodParameter.ParameterType.Assembly.GetType(type.FullName);
			}
			if (!type.IsAssignableFrom(methodParameter.ParameterType))
			{
				return false;
			}
			parameterMetadata = new ParameterMetadata(methodParameter.ParameterType, methodParameter.Name, new WireFieldMetadata[]
			{
				new WireFieldMetadata(typeof(string), new AssignedValue("PrivateInformation.ScrubIfPII({0})".FormatWithInvariantCulture(new object[] { methodParameter.Name })), ParameterFactoryUtility.GetParameterAttribute(methodParameter))
			}, new PropertyMetadata(typeof(string), methodParameter.Name));
			return true;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x000602ED File Offset: 0x0005E4ED
		public string ValidTypesAsString
		{
			get
			{
				return "types that implement the IContainsPrivateInformation interface";
			}
		}
	}
}
