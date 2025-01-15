using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200075B RID: 1883
	[Serializable]
	public class DataValueList : ArrayList
	{
		// Token: 0x06006842 RID: 26690 RVA: 0x00195C5F File Offset: 0x00193E5F
		internal DataValueList()
		{
		}

		// Token: 0x06006843 RID: 26691 RVA: 0x00195C67 File Offset: 0x00193E67
		internal DataValueList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170024D8 RID: 9432
		internal DataValue this[int index]
		{
			get
			{
				return (DataValue)base[index];
			}
		}

		// Token: 0x06006845 RID: 26693 RVA: 0x00195C80 File Offset: 0x00193E80
		internal static string CreatePropertyNameString(string prefix, int rowIndex, int cellIndex, int valueIndex)
		{
			if (rowIndex > 0)
			{
				return string.Concat(new string[]
				{
					prefix,
					"DataValue(Row:",
					rowIndex.ToString(),
					")(Cell:",
					cellIndex.ToString(),
					")(Index:",
					valueIndex.ToString(),
					")"
				});
			}
			return prefix + "CustomProperty(Index:" + valueIndex.ToString() + ")";
		}

		// Token: 0x06006846 RID: 26694 RVA: 0x00195CF6 File Offset: 0x00193EF6
		internal void Initialize(string prefix, bool isCustomProperty, InitializationContext context)
		{
			this.Initialize(prefix, -1, -1, isCustomProperty, context);
		}

		// Token: 0x06006847 RID: 26695 RVA: 0x00195D04 File Offset: 0x00193F04
		internal void Initialize(string prefix, int rowIndex, int cellIndex, bool isCustomProperty, InitializationContext context)
		{
			int count = this.Count;
			CustomPropertyUniqueNameValidator customPropertyUniqueNameValidator = new CustomPropertyUniqueNameValidator();
			for (int i = 0; i < count; i++)
			{
				Global.Tracer.Assert(this[i] != null);
				this[i].Initialize(DataValueList.CreatePropertyNameString(prefix, rowIndex + 1, cellIndex + 1, i + 1), isCustomProperty, customPropertyUniqueNameValidator, context);
			}
		}

		// Token: 0x06006848 RID: 26696 RVA: 0x00195D60 File Offset: 0x00193F60
		internal void SetExprHost(IList<DataValueExprHost> dataValueHosts, ObjectModelImpl reportObjectModel)
		{
			if (dataValueHosts != null)
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					Global.Tracer.Assert(this[i] != null);
					this[i].SetExprHost(dataValueHosts, reportObjectModel);
				}
			}
		}

		// Token: 0x06006849 RID: 26697 RVA: 0x00195DA5 File Offset: 0x00193FA5
		internal DataValueInstanceList EvaluateExpressions(ObjectType objectType, string objectName, string prefix, ReportProcessing.ProcessingContext pc)
		{
			return this.EvaluateExpressions(objectType, objectName, prefix, -1, -1, pc);
		}

		// Token: 0x0600684A RID: 26698 RVA: 0x00195DB4 File Offset: 0x00193FB4
		internal DataValueInstanceList EvaluateExpressions(ObjectType objectType, string objectName, string prefix, int rowIndex, int cellIndex, ReportProcessing.ProcessingContext pc)
		{
			int count = this.Count;
			DataValueInstanceList dataValueInstanceList = new DataValueInstanceList(count);
			bool flag = rowIndex < 0;
			CustomPropertyUniqueNameValidator customPropertyUniqueNameValidator = null;
			if (flag)
			{
				customPropertyUniqueNameValidator = new CustomPropertyUniqueNameValidator();
			}
			for (int i = 0; i < count; i++)
			{
				DataValue dataValue = this[i];
				DataValueInstance dataValueInstance = new DataValueInstance();
				bool flag2 = true;
				string text = null;
				if (dataValue.Name != null)
				{
					if (ExpressionInfo.Types.Constant != dataValue.Name.Type)
					{
						dataValueInstance.Name = pc.ReportRuntime.EvaluateDataValueNameExpression(dataValue, objectType, objectName, DataValueList.CreatePropertyNameString(prefix, rowIndex + 1, cellIndex + 1, i + 1) + ".Name");
						text = dataValueInstance.Name;
					}
					else
					{
						text = dataValue.Name.Value;
					}
				}
				if (flag)
				{
					flag2 = customPropertyUniqueNameValidator.Validate(Severity.Warning, objectType, objectName, text, pc.ErrorContext);
				}
				if (flag2)
				{
					Global.Tracer.Assert(dataValue.Value != null);
					if (ExpressionInfo.Types.Constant != dataValue.Value.Type)
					{
						dataValueInstance.Value = pc.ReportRuntime.EvaluateDataValueValueExpression(dataValue, objectType, objectName, DataValueList.CreatePropertyNameString(prefix, rowIndex + 1, cellIndex + 1, i + 1) + ".Value");
					}
				}
				dataValueInstanceList.Add(dataValueInstance);
			}
			return dataValueInstanceList;
		}

		// Token: 0x0600684B RID: 26699 RVA: 0x00195EEC File Offset: 0x001940EC
		internal DataValueList DeepClone(InitializationContext context)
		{
			int count = this.Count;
			DataValueList dataValueList = new DataValueList(count);
			for (int i = 0; i < count; i++)
			{
				dataValueList.Add(this[i].DeepClone(context));
			}
			return dataValueList;
		}
	}
}
