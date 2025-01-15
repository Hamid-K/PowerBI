using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002AD RID: 685
	internal sealed class ExtendedPropertyCollection : ReportElementCollectionBase<ExtendedProperty>
	{
		// Token: 0x06001A40 RID: 6720 RVA: 0x0006A05F File Offset: 0x0006825F
		internal ExtendedPropertyCollection(Microsoft.ReportingServices.ReportIntermediateFormat.RecordField field, List<string> extendedPropertyNames)
		{
			this.m_recordField = field;
			this.m_extendedPropertyNames = extendedPropertyNames;
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x0006A075 File Offset: 0x00068275
		public override int Count
		{
			get
			{
				if (this.m_extendedPropertyNames == null)
				{
					return 0;
				}
				return this.m_extendedPropertyNames.Count;
			}
		}

		// Token: 0x17000EFB RID: 3835
		public override ExtendedProperty this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_extendedProperties == null)
				{
					this.m_extendedProperties = new ExtendedProperty[this.Count];
				}
				if (this.m_extendedProperties[index] == null)
				{
					this.m_extendedProperties[index] = new ExtendedProperty(this.m_extendedPropertyNames[index], this.GetFieldPropertyValue(index));
				}
				return this.m_extendedProperties[index];
			}
		}

		// Token: 0x17000EFC RID: 3836
		public ExtendedProperty this[string name]
		{
			get
			{
				if (this.m_extendedPropertyNames != null)
				{
					return this[this.m_extendedPropertyNames.IndexOf(name)];
				}
				return null;
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0006A140 File Offset: 0x00068340
		internal void UpdateRecordField(Microsoft.ReportingServices.ReportIntermediateFormat.RecordField field)
		{
			this.m_recordField = field;
			if (this.m_extendedProperties != null)
			{
				for (int i = 0; i < this.m_extendedProperties.Length; i++)
				{
					ExtendedProperty extendedProperty = this.m_extendedProperties[i];
					if (extendedProperty != null)
					{
						extendedProperty.UpdateValue(this.GetFieldPropertyValue(i));
					}
				}
			}
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0006A188 File Offset: 0x00068388
		private object GetFieldPropertyValue(int index)
		{
			if (this.m_recordField == null)
			{
				return null;
			}
			return this.m_recordField.FieldPropertyValues[index];
		}

		// Token: 0x04000D14 RID: 3348
		private ExtendedProperty[] m_extendedProperties;

		// Token: 0x04000D15 RID: 3349
		private List<string> m_extendedPropertyNames;

		// Token: 0x04000D16 RID: 3350
		private Microsoft.ReportingServices.ReportIntermediateFormat.RecordField m_recordField;
	}
}
