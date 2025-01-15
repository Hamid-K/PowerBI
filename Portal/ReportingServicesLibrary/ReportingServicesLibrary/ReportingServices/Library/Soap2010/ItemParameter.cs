using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library.Soap2010
{
	// Token: 0x020002EF RID: 751
	public class ItemParameter
	{
		// Token: 0x06001ADB RID: 6875 RVA: 0x0006C614 File Offset: 0x0006A814
		public ItemParameter()
		{
			this.Name = null;
			this.ParameterTypeName = DataType.String.ToString();
			this.TypeSpecified = false;
			this.Nullable = true;
			this.NullableSpecified = false;
			this.AllowBlank = true;
			this.AllowBlankSpecified = false;
			this.MultiValue = false;
			this.MultiValueSpecified = false;
			this.QueryParameter = true;
			this.QueryParameterSpecified = false;
			this.Prompt = null;
			this.PromptUser = true;
			this.PromptUserSpecified = false;
			this.Dependencies = null;
			this.ValidValuesQueryBased = false;
			this.ValidValuesQueryBasedSpecified = false;
			this.ValidValues = null;
			this.DefaultValuesQueryBased = false;
			this.DefaultValuesQueryBasedSpecified = false;
			this.DefaultValues = null;
			this.ParameterStateName = ParameterStateEnum.MissingValidValue.ToString();
			this.StateSpecified = false;
			this.ErrorMessage = null;
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0006C6EC File Offset: 0x0006A8EC
		internal static ParameterInfoCollection ParameterArrayToCollection(ItemParameter[] parameters)
		{
			if (parameters == null)
			{
				return null;
			}
			ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection();
			foreach (ItemParameter itemParameter in parameters)
			{
				if (itemParameter == null)
				{
					throw new MissingElementException("ItemParameter");
				}
				ParameterInfo parameterInfo = new ParameterInfo();
				parameterInfo.Name = itemParameter.Name;
				parameterInfo.Prompt = itemParameter.Prompt;
				parameterInfo.DataType = DataType.String;
				if (itemParameter.PromptUserSpecified)
				{
					parameterInfo.PromptUser = itemParameter.PromptUser;
				}
				if (itemParameter.DefaultValues != null)
				{
					parameterInfo.UseExplicitDefaultValue = true;
					parameterInfo.Values = new object[itemParameter.DefaultValues.Length];
					Array.Copy(itemParameter.DefaultValues, parameterInfo.Values, itemParameter.DefaultValues.Length);
					parameterInfo.DefaultValues = new object[itemParameter.DefaultValues.Length];
					Array.Copy(itemParameter.DefaultValues, parameterInfo.DefaultValues, itemParameter.DefaultValues.Length);
					if (itemParameter.DefaultValuesQueryBasedSpecified)
					{
						parameterInfo.DynamicDefaultValue = itemParameter.DefaultValuesQueryBased;
					}
				}
				else if (itemParameter.DefaultValuesQueryBasedSpecified)
				{
					parameterInfo.UseExplicitDefaultValue = true;
					parameterInfo.DynamicDefaultValue = itemParameter.DefaultValuesQueryBased;
				}
				parameterInfoCollection.Add(parameterInfo);
			}
			return parameterInfoCollection;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0006C804 File Offset: 0x0006AA04
		internal static ItemParameter[] CollectionToParameterArray(ParameterInfoCollection parameterCollection)
		{
			return ItemParameter.CollectionToParameterArray(parameterCollection, false);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0006C810 File Offset: 0x0006AA10
		internal static ItemParameter[] CollectionToParameterArray(ParameterInfoCollection parameterCollection, bool isDataSetParameter)
		{
			if (parameterCollection == null)
			{
				return null;
			}
			List<ItemParameter> list = new List<ItemParameter>();
			foreach (object obj in parameterCollection)
			{
				ParameterInfo parameterInfo = (ParameterInfo)obj;
				if (!isDataSetParameter || parameterInfo.PromptUser)
				{
					ItemParameter itemParameter = new ItemParameter();
					itemParameter.Name = parameterInfo.Name;
					itemParameter.TypeSpecified = true;
					DataType dataType = parameterInfo.DataType;
					if (dataType == DataType.Object)
					{
						RSTrace.CatalogTrace.Assert(isDataSetParameter, "Parameter type object in a report");
						itemParameter.ParameterTypeName = DataType.String.ToString();
					}
					else
					{
						RSTrace.CatalogTrace.Assert(!isDataSetParameter, "Typed parameter in shared dataset");
						itemParameter.ParameterTypeName = parameterInfo.DataType.ToString();
					}
					if (!isDataSetParameter)
					{
						itemParameter.AllowBlankSpecified = true;
						itemParameter.AllowBlank = parameterInfo.AllowBlank;
						itemParameter.DefaultValuesQueryBasedSpecified = true;
						itemParameter.DefaultValuesQueryBased = parameterInfo.DynamicDefaultValue;
						if (parameterInfo.DependencyList != null)
						{
							itemParameter.Dependencies = new string[parameterInfo.DependencyList.Count];
							for (int i = 0; i < parameterInfo.DependencyList.Count; i++)
							{
								itemParameter.Dependencies[i] = parameterInfo.DependencyList[i].Name;
							}
						}
						itemParameter.MultiValueSpecified = true;
						itemParameter.MultiValue = parameterInfo.MultiValue;
						itemParameter.ValidValuesQueryBasedSpecified = true;
						itemParameter.ValidValuesQueryBased = parameterInfo.DynamicValidValues;
						if (parameterInfo.ValidValues != null)
						{
							itemParameter.ValidValues = new Microsoft.ReportingServices.Library.Soap.ValidValue[parameterInfo.ValidValues.Count];
							for (int j = 0; j < parameterInfo.ValidValues.Count; j++)
							{
								Microsoft.ReportingServices.Library.Soap.ValidValue validValue = new Microsoft.ReportingServices.Library.Soap.ValidValue();
								validValue.Label = parameterInfo.ValidValues[j].Label;
								validValue.Value = parameterInfo.CastToString(parameterInfo.ValidValues[j].Value, Localization.ClientPrimaryCulture);
								itemParameter.ValidValues[j] = validValue;
							}
						}
					}
					itemParameter.Prompt = parameterInfo.Prompt;
					itemParameter.PromptUserSpecified = true;
					itemParameter.PromptUser = parameterInfo.PromptUser;
					itemParameter.NullableSpecified = true;
					itemParameter.Nullable = parameterInfo.Nullable;
					itemParameter.QueryParameterSpecified = true;
					itemParameter.QueryParameter = parameterInfo.UsedInQuery;
					if (parameterInfo.Values != null)
					{
						itemParameter.DefaultValues = new string[parameterInfo.Values.Length];
						for (int k = 0; k < parameterInfo.Values.Length; k++)
						{
							itemParameter.DefaultValues[k] = parameterInfo.CastToString(parameterInfo.Values[k], Localization.ClientPrimaryCulture);
						}
					}
					else if (parameterInfo.DefaultValues != null)
					{
						itemParameter.DefaultValues = new string[parameterInfo.DefaultValues.Length];
						for (int l = 0; l < parameterInfo.DefaultValues.Length; l++)
						{
							itemParameter.DefaultValues[l] = parameterInfo.CastToString(parameterInfo.DefaultValues[l], Localization.ClientPrimaryCulture);
						}
					}
					itemParameter.StateSpecified = true;
					itemParameter.ParameterStateName = ReportParameter.ToSoapParameterStateEnum(parameterInfo.State).ToString();
					list.Add(itemParameter);
				}
			}
			return list.ToArray();
		}

		// Token: 0x040009B7 RID: 2487
		public string Name;

		// Token: 0x040009B8 RID: 2488
		public string ParameterTypeName;

		// Token: 0x040009B9 RID: 2489
		[XmlIgnore]
		public bool TypeSpecified;

		// Token: 0x040009BA RID: 2490
		public bool Nullable;

		// Token: 0x040009BB RID: 2491
		[XmlIgnore]
		public bool NullableSpecified;

		// Token: 0x040009BC RID: 2492
		public bool AllowBlank;

		// Token: 0x040009BD RID: 2493
		[XmlIgnore]
		public bool AllowBlankSpecified;

		// Token: 0x040009BE RID: 2494
		public bool MultiValue;

		// Token: 0x040009BF RID: 2495
		[XmlIgnore]
		public bool MultiValueSpecified;

		// Token: 0x040009C0 RID: 2496
		public bool QueryParameter;

		// Token: 0x040009C1 RID: 2497
		[XmlIgnore]
		public bool QueryParameterSpecified;

		// Token: 0x040009C2 RID: 2498
		public string Prompt;

		// Token: 0x040009C3 RID: 2499
		public bool PromptUser;

		// Token: 0x040009C4 RID: 2500
		[XmlIgnore]
		public bool PromptUserSpecified;

		// Token: 0x040009C5 RID: 2501
		[XmlArrayItem("Dependency")]
		public string[] Dependencies;

		// Token: 0x040009C6 RID: 2502
		public bool ValidValuesQueryBased;

		// Token: 0x040009C7 RID: 2503
		[XmlIgnore]
		public bool ValidValuesQueryBasedSpecified;

		// Token: 0x040009C8 RID: 2504
		public Microsoft.ReportingServices.Library.Soap.ValidValue[] ValidValues;

		// Token: 0x040009C9 RID: 2505
		public bool DefaultValuesQueryBased;

		// Token: 0x040009CA RID: 2506
		[XmlIgnore]
		public bool DefaultValuesQueryBasedSpecified;

		// Token: 0x040009CB RID: 2507
		[XmlArrayItem("Value")]
		public string[] DefaultValues;

		// Token: 0x040009CC RID: 2508
		public string ParameterStateName;

		// Token: 0x040009CD RID: 2509
		[XmlIgnore]
		public bool StateSpecified;

		// Token: 0x040009CE RID: 2510
		public string ErrorMessage;

		// Token: 0x040009CF RID: 2511
		private const string ElementName = "ItemParameter";
	}
}
