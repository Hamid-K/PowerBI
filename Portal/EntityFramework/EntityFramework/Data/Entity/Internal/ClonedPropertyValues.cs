using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000FF RID: 255
	internal class ClonedPropertyValues : InternalPropertyValues
	{
		// Token: 0x0600127E RID: 4734 RVA: 0x00030764 File Offset: 0x0002E964
		internal ClonedPropertyValues(InternalPropertyValues original, DbDataRecord valuesRecord = null)
			: base(original.InternalContext, original.ObjectType, original.IsEntityValues)
		{
			this._propertyNames = original.PropertyNames;
			this._propertyValues = new Dictionary<string, ClonedPropertyValuesItem>(this._propertyNames.Count);
			foreach (string text in this._propertyNames)
			{
				IPropertyValuesItem item = original.GetItem(text);
				object obj = item.Value;
				InternalPropertyValues internalPropertyValues = obj as InternalPropertyValues;
				if (internalPropertyValues != null)
				{
					DbDataRecord dbDataRecord = ((valuesRecord == null) ? null : ((DbDataRecord)valuesRecord[text]));
					obj = new ClonedPropertyValues(internalPropertyValues, dbDataRecord);
				}
				else if (valuesRecord != null)
				{
					obj = valuesRecord[text];
					if (obj == DBNull.Value)
					{
						obj = null;
					}
				}
				this._propertyValues[text] = new ClonedPropertyValuesItem(text, obj, item.Type, item.IsComplex);
			}
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00030854 File Offset: 0x0002EA54
		protected override IPropertyValuesItem GetItemImpl(string propertyName)
		{
			return this._propertyValues[propertyName];
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x00030862 File Offset: 0x0002EA62
		public override ISet<string> PropertyNames
		{
			get
			{
				return this._propertyNames;
			}
		}

		// Token: 0x04000920 RID: 2336
		private readonly ISet<string> _propertyNames;

		// Token: 0x04000921 RID: 2337
		private readonly IDictionary<string, ClonedPropertyValuesItem> _propertyValues;
	}
}
