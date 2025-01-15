using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000367 RID: 871
	internal class WireTypeParameterFactory : IParameterFactory
	{
		// Token: 0x060019F7 RID: 6647 RVA: 0x0006009C File Offset: 0x0005E29C
		public bool TryCreateParameterMetadata(ParameterInfo methodParameter, out ParameterMetadata parameterMetadata)
		{
			PropertyInfo wireTypePropertyInfo = WireTypeParameterFactory.GetWireTypePropertyInfo(methodParameter.ParameterType);
			parameterMetadata = null;
			if (wireTypePropertyInfo == null)
			{
				return false;
			}
			parameterMetadata = new ParameterMetadata(methodParameter.ParameterType, methodParameter.Name, new WireFieldMetadata[]
			{
				new WireFieldMetadata(wireTypePropertyInfo.PropertyType, new AssignedValue(methodParameter.Name, wireTypePropertyInfo.Name), ParameterFactoryUtility.GetParameterAttribute(methodParameter))
			}, new PropertyMetadata(wireTypePropertyInfo.PropertyType, methodParameter.Name));
			return true;
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x00060113 File Offset: 0x0005E313
		public string ValidTypesAsString
		{
			get
			{
				return "types that have a [WireType] property";
			}
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0006011C File Offset: 0x0005E31C
		private static PropertyInfo GetWireTypePropertyInfo(Type type)
		{
			IEnumerable<PropertyInfo> enumerable = from pi in type.GetProperties()
				from at in CustomAttributeData.GetCustomAttributes(pi)
				where at.Constructor.DeclaringType.FullName.Equals(typeof(WireTypeAttribute).FullName, StringComparison.Ordinal)
				select pi;
			if (enumerable.Count<PropertyInfo>() > 1)
			{
				throw new InvalidOperationException(type.FullName + " must have at most one [WireType] attribute.");
			}
			PropertyInfo propertyInfo = enumerable.FirstOrDefault<PropertyInfo>();
			if (propertyInfo != null && !WireFieldMetadata.IsKnownType(propertyInfo.PropertyType))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "{0} is not a simple type in {1}.", new object[] { propertyInfo.PropertyType, type.FullName }));
			}
			return propertyInfo;
		}
	}
}
