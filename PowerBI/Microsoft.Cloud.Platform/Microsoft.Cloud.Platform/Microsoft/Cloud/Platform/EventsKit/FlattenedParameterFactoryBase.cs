using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000354 RID: 852
	public abstract class FlattenedParameterFactoryBase : IParameterFactory
	{
		// Token: 0x06001941 RID: 6465 RVA: 0x0005DE6A File Offset: 0x0005C06A
		protected FlattenedParameterFactoryBase(ReadOnlyCollection<FlattenedPropertyMetadata> properties, Type compoundType, Type flattenedCompoundType, string validType)
		{
			FlattenedParameterFactoryBase.EnsureFlattenedTypeIsValid(flattenedCompoundType, properties);
			this.m_properties = properties;
			this.m_compoundType = compoundType;
			this.m_flattenedCompoundType = flattenedCompoundType;
			this.ValidTypesAsString = validType;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0005DE98 File Offset: 0x0005C098
		private static void EnsureFlattenedTypeIsValid(Type flattenedType, ReadOnlyCollection<FlattenedPropertyMetadata> properties)
		{
			ParameterInfo[] parameters = flattenedType.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0].GetParameters();
			for (int i = 0; i < properties.Count; i++)
			{
				FlattenedPropertyMetadata flattenedPropertyMetadata = properties[i];
				ParameterInfo parameterInfo = parameters[i];
				if (!flattenedPropertyMetadata.ConstructorInitializerName.Equals(parameterInfo.Name, StringComparison.Ordinal))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The {0} constructor parameter number {1} should be renamed from {2} to {3}", new object[]
					{
						flattenedType.Name,
						i + 1,
						parameterInfo.Name,
						flattenedPropertyMetadata.ConstructorInitializerName
					}));
				}
				if (!flattenedPropertyMetadata.VariableMetadataType.Equals(parameterInfo.ParameterType))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The {0} constructor parameter number {1} should have the type changed from {2} to {3}", new object[]
					{
						flattenedType.Name,
						i + 1,
						parameterInfo.ParameterType,
						flattenedPropertyMetadata.VariableMetadataType
					}));
				}
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0005DF84 File Offset: 0x0005C184
		public bool TryCreateParameterMetadata(ParameterInfo methodParameter, out ParameterMetadata parameterMetadata)
		{
			parameterMetadata = null;
			if (ParameterFactoryUtility.GetParameterAttribute(methodParameter) != null)
			{
				return false;
			}
			Type type = this.m_compoundType;
			if (methodParameter.ParameterType.Assembly.ReflectionOnly)
			{
				type = methodParameter.ParameterType.Assembly.GetType(this.m_compoundType.FullName);
				if (type == null)
				{
					type = Assembly.ReflectionOnlyLoad(this.m_compoundType.Assembly.FullName).GetType(this.m_compoundType.FullName);
				}
			}
			else if (methodParameter.ParameterType.Assembly.FullName == this.m_compoundType.Assembly.FullName)
			{
				type = methodParameter.ParameterType.Assembly.GetType(this.m_compoundType.FullName);
			}
			if (!type.IsAssignableFrom(methodParameter.ParameterType))
			{
				return false;
			}
			Collection<WireFieldMetadata> collection = new Collection<WireFieldMetadata>();
			foreach (FlattenedPropertyMetadata flattenedPropertyMetadata in this.m_properties)
			{
				collection.Add(new WireFieldMetadata(flattenedPropertyMetadata.VariableMetadataType, new AssignedValue(methodParameter.Name, flattenedPropertyMetadata.Name), null));
			}
			parameterMetadata = new ParameterMetadata(methodParameter.ParameterType, methodParameter.Name, collection, new PropertyMetadata(this.m_flattenedCompoundType, methodParameter.Name));
			return true;
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x0005E0E0 File Offset: 0x0005C2E0
		// (set) Token: 0x06001945 RID: 6469 RVA: 0x0005E0E8 File Offset: 0x0005C2E8
		public string ValidTypesAsString { get; private set; }

		// Token: 0x040008B5 RID: 2229
		private readonly Type m_compoundType;

		// Token: 0x040008B6 RID: 2230
		private readonly IEnumerable<FlattenedPropertyMetadata> m_properties;

		// Token: 0x040008B7 RID: 2231
		private readonly Type m_flattenedCompoundType;
	}
}
