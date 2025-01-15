using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200007D RID: 125
	internal abstract class PropertyCollection
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x0001588A File Offset: 0x00013A8A
		protected PropertyCollection()
		{
			this.m_properties = new NameValueCollection();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000158A0 File Offset: 0x00013AA0
		protected PropertyCollection(Property[] properties)
		{
			this.m_properties = new NameValueCollection();
			if (properties == null || properties.Length == 0)
			{
				return;
			}
			foreach (Property property in properties)
			{
				if (property == null)
				{
					throw new MissingElementException("Property");
				}
				string name = property.Name;
				string value = property.Value;
				if (name == null)
				{
					throw new MissingElementException("Name");
				}
				if (value == null || value.Length == 0)
				{
					this.m_properties[name] = null;
				}
				else
				{
					this.m_properties[name] = value;
				}
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00015927 File Offset: 0x00013B27
		protected PropertyCollection(string propertiesXml)
		{
			this.m_properties = XmlUtil.ShallowXmlToNameValueCollection(propertiesXml, "Properties");
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00015940 File Offset: 0x00013B40
		internal Property[] FilterProperties(Property[] requestedProperties)
		{
			if (requestedProperties == null)
			{
				return this.ToSoapCollection();
			}
			ArrayList arrayList = new ArrayList();
			foreach (Property property in requestedProperties)
			{
				if (property == null)
				{
					throw new MissingElementException("Property");
				}
				string name = property.Name;
				if (name == null)
				{
					throw new MissingElementException("Name");
				}
				string text = this.m_properties[name];
				if (text != null)
				{
					arrayList.Add(new Property
					{
						Name = name,
						Value = text
					});
				}
			}
			return (Property[])arrayList.ToArray(typeof(Property));
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000159D8 File Offset: 0x00013BD8
		internal void CombineProperties(PropertyCollection propertiesToSet)
		{
			for (int i = 0; i < propertiesToSet.Count; i++)
			{
				string name = propertiesToSet.GetName(i);
				string value = propertiesToSet.GetValue(i);
				this[name] = value;
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00015A10 File Offset: 0x00013C10
		internal void EnsurePropertiesWritable()
		{
			for (int i = 0; i < this.Count; i++)
			{
				string name = this.GetName(i);
				if (this.IsReadOnlyProperty(name))
				{
					throw new ReadOnlyPropertyException(name);
				}
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00015A46 File Offset: 0x00013C46
		protected string ToXml()
		{
			return XmlUtil.NameValueCollectionToShallowXml(this.m_properties, "Properties");
		}

		// Token: 0x0600054C RID: 1356
		protected abstract bool IsReadOnlyProperty(string propertyName);

		// Token: 0x0600054D RID: 1357 RVA: 0x00015A58 File Offset: 0x00013C58
		private Property[] ToSoapCollection()
		{
			Property[] array = new Property[this.m_properties.Count];
			for (int i = 0; i < this.m_properties.Count; i++)
			{
				array[i] = new Property
				{
					Name = this.GetName(i),
					Value = this.GetValue(i)
				};
			}
			return array;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00015AB4 File Offset: 0x00013CB4
		protected void ValidateReportTimeoutIfPresent(string timeoutPropertyName, ItemType itemType)
		{
			string text = this[timeoutPropertyName];
			if (text == null)
			{
				return;
			}
			if (itemType != ItemType.Report && itemType != ItemType.LinkedReport && itemType != ItemType.RdlxReport && itemType != ItemType.Unknown)
			{
				throw new InvalidElementException(timeoutPropertyName);
			}
			int num;
			try
			{
				num = int.Parse(text, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				throw new ElementTypeMismatchException(timeoutPropertyName);
			}
			catch (OverflowException)
			{
				throw new ElementTypeMismatchException(timeoutPropertyName);
			}
			if (num < 1 && num != -1)
			{
				throw new InvalidElementException(timeoutPropertyName);
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00015B30 File Offset: 0x00013D30
		protected void ValidateIntegerProperty(string name, string val, bool throwIfNull)
		{
			if (val != null)
			{
				try
				{
					int.Parse(val, CultureInfo.InvariantCulture);
				}
				catch (FormatException)
				{
					throw new ElementTypeMismatchException(name);
				}
				catch (OverflowException)
				{
					throw new ElementTypeMismatchException(name);
				}
				return;
			}
			if (throwIfNull)
			{
				throw new MissingElementException(name);
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00015B88 File Offset: 0x00013D88
		protected void ValidateBooleanProperty(string name, string val, bool throwIfNull)
		{
			if (val != null)
			{
				try
				{
					bool.Parse(val);
				}
				catch (FormatException)
				{
					throw new ElementTypeMismatchException(name);
				}
				return;
			}
			if (throwIfNull)
			{
				throw new MissingElementException(name);
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00015BC8 File Offset: 0x00013DC8
		protected void ValidateDoubleProperty(string name, string val)
		{
			if (val == null)
			{
				return;
			}
			double num;
			if (!double.TryParse(val, NumberStyles.Number, CultureInfo.InvariantCulture, out num))
			{
				throw new ElementTypeMismatchException(name);
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00015BF4 File Offset: 0x00013DF4
		protected void ValidateEnumProperty<TEnum>(string name, string value)
		{
			if (value == null)
			{
				return;
			}
			try
			{
				Enum.Parse(typeof(TEnum), value, true);
			}
			catch (ArgumentException)
			{
				throw new ElementTypeMismatchException(name);
			}
		}

		// Token: 0x170001B3 RID: 435
		internal string this[string name]
		{
			get
			{
				return this.m_properties[name];
			}
			set
			{
				this.m_properties[name] = value;
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00015C51 File Offset: 0x00013E51
		internal void Remove(string name)
		{
			this.m_properties.Remove(name);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00015C5F File Offset: 0x00013E5F
		internal string GetName(int i)
		{
			return this.m_properties.GetKey(i);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00015C6D File Offset: 0x00013E6D
		internal string GetValue(int i)
		{
			return this.m_properties.Get(i);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00015C34 File Offset: 0x00013E34
		protected string GetValue(string name)
		{
			return this.m_properties[name];
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00015C7B File Offset: 0x00013E7B
		internal int Count
		{
			get
			{
				return this.m_properties.Count;
			}
		}

		// Token: 0x04000260 RID: 608
		private NameValueCollection m_properties;

		// Token: 0x04000261 RID: 609
		internal const string PropertiesTag = "Properties";
	}
}
