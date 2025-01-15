using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Tmdl;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001CC RID: 460
	internal static class TmdlObjectExtensions
	{
		// Token: 0x06001BDF RID: 7135 RVA: 0x000C343C File Offset: 0x000C163C
		public static TmdlObject WithName(this TmdlObject tmdlObject, ObjectName name)
		{
			tmdlObject.Name = name;
			return tmdlObject;
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x000C3446 File Offset: 0x000C1646
		public static TmdlObject WithName(this TmdlObject tmdlObject, params string[] nameParts)
		{
			tmdlObject.Name = new ObjectName(nameParts);
			return tmdlObject;
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x000C3455 File Offset: 0x000C1655
		public static TmdlObject WithSourceLocation(this TmdlObject tmdlObject, TmdlSourceLocation location)
		{
			tmdlObject.SourceLocation = location;
			return tmdlObject;
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x000C345F File Offset: 0x000C165F
		public static TmdlObject WithDefaultProperty(this TmdlObject tmdlObject, TmdlProperty property)
		{
			tmdlObject.DefaultProperty = property;
			return tmdlObject;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000C346C File Offset: 0x000C166C
		public static TmdlObject WithProperties(this TmdlObject tmdlObject, params TmdlProperty[] properties)
		{
			if (properties != null && properties.Length != 0)
			{
				foreach (TmdlProperty tmdlProperty in properties)
				{
					tmdlObject.Properties.Add(tmdlProperty);
				}
			}
			return tmdlObject;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000C34A4 File Offset: 0x000C16A4
		public static TmdlObject WithProperties(this TmdlObject tmdlObject, IEnumerable<TmdlProperty> properties)
		{
			foreach (TmdlProperty tmdlProperty in properties)
			{
				tmdlObject.Properties.Add(tmdlProperty);
			}
			return tmdlObject;
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x000C34F4 File Offset: 0x000C16F4
		public static TmdlObject WithDeprecatedProperties(this TmdlObject tmdlObject, params TmdlProperty[] properties)
		{
			if (properties != null && properties.Length != 0)
			{
				foreach (TmdlProperty tmdlProperty in properties)
				{
					tmdlObject.AddDeprecatedProperty(tmdlProperty);
				}
			}
			return tmdlObject;
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000C3524 File Offset: 0x000C1724
		public static TmdlObject WithDeprecatedProperties(this TmdlObject tmdlObject, IEnumerable<TmdlProperty> properties)
		{
			foreach (TmdlProperty tmdlProperty in properties)
			{
				tmdlObject.AddDeprecatedProperty(tmdlProperty);
			}
			return tmdlObject;
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000C3570 File Offset: 0x000C1770
		public static TmdlObject WithChildObjects(this TmdlObject tmdlObject, params TmdlObject[] objects)
		{
			if (objects != null && objects.Length != 0)
			{
				foreach (TmdlObject tmdlObject2 in objects)
				{
					tmdlObject.Children.Add(tmdlObject2);
				}
			}
			return tmdlObject;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000C35A8 File Offset: 0x000C17A8
		public static TmdlObject WithChildObjects(this TmdlObject tmdlObject, IEnumerable<TmdlObject> objects)
		{
			foreach (TmdlObject tmdlObject2 in objects)
			{
				tmdlObject.Children.Add(tmdlObject2);
			}
			return tmdlObject;
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x000C35F8 File Offset: 0x000C17F8
		public static T? GetScalarPropertyValue<T>(this TmdlObject tmdlObject, string propertyName) where T : struct
		{
			TmdlProperty propertyByName = tmdlObject.GetPropertyByName(propertyName, StringComparison.InvariantCulture);
			if (propertyByName == null)
			{
				throw new ArgumentException(TomSR.Exception_TmdlPropertyNotExist(propertyName), "propertyName");
			}
			TmdlScalarValue<T> tmdlScalarValue = propertyByName.Value as TmdlScalarValue<T>;
			if (tmdlScalarValue == null)
			{
				throw new ArgumentException(TomSR.Exception_TmdlPropertyMismatchScalarType(propertyName, typeof(T).Name, propertyByName.Value.Type.ToString("G"), propertyByName.Value.GetTypeCode().ToString()), "propertyName");
			}
			return tmdlScalarValue.GetValue();
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x000C3688 File Offset: 0x000C1888
		public static T GetPropertyValueOrDefault<T>(this TmdlObject tmdlObject, string propertyName) where T : struct
		{
			TmdlProperty propertyByName = tmdlObject.GetPropertyByName(propertyName, StringComparison.InvariantCulture);
			if (propertyByName != null)
			{
				TmdlScalarValue<T> tmdlScalarValue = propertyByName.Value as TmdlScalarValue<T>;
				if (tmdlScalarValue != null && tmdlScalarValue.GetValue() != null)
				{
					return tmdlScalarValue.GetValue().Value;
				}
			}
			return default(T);
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000C36D8 File Offset: 0x000C18D8
		internal static TmdlProperty GetPropertyByName(this TmdlObject tmdlObject, string propertyName, StringComparison comparison = StringComparison.InvariantCulture)
		{
			if (tmdlObject.DefaultProperty != null && propertyName.Equals(tmdlObject.DefaultProperty.Name, comparison))
			{
				return tmdlObject.DefaultProperty;
			}
			if (tmdlObject.HasAnyProperty(false))
			{
				return tmdlObject.Properties.FirstOrDefault((TmdlProperty p) => propertyName.Equals(p.Name, comparison));
			}
			return null;
		}
	}
}
