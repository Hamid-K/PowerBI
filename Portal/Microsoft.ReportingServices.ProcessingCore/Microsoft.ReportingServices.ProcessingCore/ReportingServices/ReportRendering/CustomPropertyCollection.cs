using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200005F RID: 95
	public sealed class CustomPropertyCollection
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x0001958D File Offset: 0x0001778D
		public CustomPropertyCollection()
		{
			this.m_isCustomControl = true;
			this.m_instances = new DataValueInstanceList();
			this.m_uniqueNames = new Hashtable();
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x000195B4 File Offset: 0x000177B4
		internal CustomPropertyCollection(DataValueList expressions, DataValueInstanceList instances)
		{
			this.m_expressions = expressions;
			this.m_instances = instances;
			Global.Tracer.Assert(this.m_expressions != null);
			Global.Tracer.Assert(this.m_instances == null || this.m_instances.Count == this.m_expressions.Count);
			this.m_uniqueNames = new Hashtable(this.m_expressions.Count);
			this.m_expressionIndex = new IntList(this.m_expressions.Count);
		}

		// Token: 0x170004F9 RID: 1273
		public CustomProperty this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterValue, new object[] { "name" });
				}
				if (!this.m_populated && !this.m_isCustomControl)
				{
					this.Populate();
				}
				object obj = this.m_uniqueNames[name];
				if (obj != null && obj is int)
				{
					string text;
					object obj2;
					this.GetNameValue((int)obj, out text, out obj2);
					return new CustomProperty(text, obj2);
				}
				return null;
			}
		}

		// Token: 0x170004FA RID: 1274
		public CustomProperty this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				string text = null;
				object obj = null;
				if (this.IsCustomControl)
				{
					text = this.m_instances[index].Name;
					obj = this.m_instances[index].Value;
				}
				else
				{
					if (!this.m_populated)
					{
						this.Populate();
					}
					Global.Tracer.Assert(this.m_expressionIndex.Count <= this.m_expressions.Count && index <= this.m_expressionIndex.Count);
					this.GetNameValue(this.m_expressionIndex[index], out text, out obj);
				}
				return new CustomProperty(text, obj);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00019792 File Offset: 0x00017992
		public int Count
		{
			get
			{
				if (!this.m_populated && !this.m_isCustomControl)
				{
					this.Populate();
				}
				return this.m_uniqueNames.Count;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x000197B5 File Offset: 0x000179B5
		internal bool IsCustomControl
		{
			get
			{
				return this.m_isCustomControl;
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000197BD File Offset: 0x000179BD
		public void Add(string propertyName, object propertyValue)
		{
			if (!this.m_isCustomControl)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			this.InternalAdd(propertyName, propertyValue);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000197DC File Offset: 0x000179DC
		public void Add(CustomProperty property)
		{
			if (!this.m_isCustomControl)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			if (property == null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterValue, new object[] { "property" });
			}
			this.InternalAdd(property.Name, property.Value);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001982C File Offset: 0x00017A2C
		internal CustomPropertyCollection DeepClone()
		{
			Global.Tracer.Assert(this.m_isCustomControl && this.m_expressions == null);
			CustomPropertyCollection customPropertyCollection = new CustomPropertyCollection();
			if (this.m_instances != null)
			{
				int count = this.m_instances.Count;
				customPropertyCollection.m_instances = new DataValueInstanceList(count);
				for (int i = 0; i < count; i++)
				{
					customPropertyCollection.m_instances.Add(this.m_instances[i].DeepClone());
				}
			}
			return customPropertyCollection;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000198A8 File Offset: 0x00017AA8
		private void Populate()
		{
			Global.Tracer.Assert(!this.m_isCustomControl);
			int count = this.m_expressions.Count;
			for (int i = 0; i < count; i++)
			{
				string text;
				object obj;
				this.GetNameValue(i, out text, out obj);
				if (text != null && !this.m_uniqueNames.ContainsKey(text))
				{
					this.m_uniqueNames.Add(text, i);
					this.m_expressionIndex.Add(i);
				}
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00019920 File Offset: 0x00017B20
		internal void GetNameValue(int index, out string name, out object value)
		{
			name = null;
			value = null;
			Global.Tracer.Assert(0 <= index && index < this.m_expressions.Count);
			if (ExpressionInfo.Types.Constant == this.m_expressions[index].Name.Type)
			{
				name = this.m_expressions[index].Name.Value;
			}
			else if (this.m_instances != null)
			{
				name = this.m_instances[index].Name;
			}
			if (ExpressionInfo.Types.Constant == this.m_expressions[index].Value.Type)
			{
				value = this.m_expressions[index].Value.Value;
				return;
			}
			if (this.m_instances != null)
			{
				value = this.m_instances[index].Value;
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000199ED File Offset: 0x00017BED
		internal void GetNameValueExpressions(int index, out ExpressionInfo nameExpression, out ExpressionInfo valueExpression, out string name, out object value)
		{
			this.GetNameValue(index, out name, out value);
			nameExpression = this.m_expressions[index].Name;
			valueExpression = this.m_expressions[index].Value;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00019A20 File Offset: 0x00017C20
		private void InternalAdd(string name, object value)
		{
			DataValueInstance dataValueInstance = new DataValueInstance();
			dataValueInstance.Name = name;
			dataValueInstance.Value = value;
			this.m_uniqueNames.Add(name, dataValueInstance);
			this.m_instances.Add(dataValueInstance);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00019A5B File Offset: 0x00017C5B
		internal DataValueInstanceList Deconstruct()
		{
			return this.m_instances;
		}

		// Token: 0x040001B8 RID: 440
		private DataValueInstanceList m_instances;

		// Token: 0x040001B9 RID: 441
		private DataValueList m_expressions;

		// Token: 0x040001BA RID: 442
		private bool m_isCustomControl;

		// Token: 0x040001BB RID: 443
		private bool m_populated;

		// Token: 0x040001BC RID: 444
		private Hashtable m_uniqueNames;

		// Token: 0x040001BD RID: 445
		private IntList m_expressionIndex;
	}
}
