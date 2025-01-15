using System;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000320 RID: 800
	public class ReportParameter
	{
		// Token: 0x06001B44 RID: 6980 RVA: 0x0006EE2C File Offset: 0x0006D02C
		public ReportParameter()
		{
			this.Name = null;
			this.Type = ParameterTypeEnum.String;
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
			this.State = ParameterStateEnum.MissingValidValue;
			this.StateSpecified = false;
			this.ErrorMessage = null;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0006EEE8 File Offset: 0x0006D0E8
		internal static ParameterInfoCollection ParameterArrayToCollection(ReportParameter[] parameters)
		{
			if (parameters == null)
			{
				return null;
			}
			ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection();
			foreach (ReportParameter reportParameter in parameters)
			{
				if (reportParameter == null)
				{
					throw new MissingParameterException("ReportParameter");
				}
				ParameterInfo parameterInfo = new ParameterInfo();
				parameterInfo.Name = reportParameter.Name;
				parameterInfo.Prompt = reportParameter.Prompt;
				parameterInfo.DataType = DataType.String;
				if (reportParameter.PromptUserSpecified)
				{
					parameterInfo.PromptUser = reportParameter.PromptUser;
				}
				if (reportParameter.DefaultValues != null)
				{
					parameterInfo.UseExplicitDefaultValue = true;
					parameterInfo.Values = new object[reportParameter.DefaultValues.Length];
					Array.Copy(reportParameter.DefaultValues, parameterInfo.Values, reportParameter.DefaultValues.Length);
					parameterInfo.DefaultValues = new object[reportParameter.DefaultValues.Length];
					Array.Copy(reportParameter.DefaultValues, parameterInfo.DefaultValues, reportParameter.DefaultValues.Length);
					if (reportParameter.DefaultValuesQueryBasedSpecified)
					{
						parameterInfo.DynamicDefaultValue = reportParameter.DefaultValuesQueryBased;
					}
				}
				else if (reportParameter.DefaultValuesQueryBasedSpecified)
				{
					parameterInfo.UseExplicitDefaultValue = true;
					parameterInfo.DynamicDefaultValue = reportParameter.DefaultValuesQueryBased;
				}
				parameterInfoCollection.Add(parameterInfo);
			}
			return parameterInfoCollection;
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0006F000 File Offset: 0x0006D200
		internal static ReportParameter[] CollectionToParameterArray(ParameterInfoCollection parameterCollection)
		{
			if (parameterCollection == null)
			{
				return null;
			}
			ReportParameter[] array = new ReportParameter[parameterCollection.Count];
			for (int i = 0; i < parameterCollection.Count; i++)
			{
				ParameterInfo parameterInfo = parameterCollection[i];
				ReportParameter reportParameter = new ReportParameter();
				reportParameter.Name = parameterInfo.Name;
				reportParameter.TypeSpecified = true;
				reportParameter.Type = (ParameterTypeEnum)Enum.Parse(typeof(ParameterTypeEnum), parameterInfo.DataType.ToString());
				reportParameter.NullableSpecified = true;
				reportParameter.Nullable = parameterInfo.Nullable;
				reportParameter.AllowBlankSpecified = true;
				reportParameter.AllowBlank = parameterInfo.AllowBlank;
				reportParameter.MultiValueSpecified = true;
				reportParameter.MultiValue = parameterInfo.MultiValue;
				reportParameter.QueryParameterSpecified = true;
				reportParameter.QueryParameter = parameterInfo.UsedInQuery;
				reportParameter.Prompt = parameterInfo.Prompt;
				reportParameter.PromptUserSpecified = true;
				reportParameter.PromptUser = parameterInfo.PromptUser;
				if (parameterInfo.DependencyList != null)
				{
					reportParameter.Dependencies = new string[parameterInfo.DependencyList.Count];
					for (int j = 0; j < parameterInfo.DependencyList.Count; j++)
					{
						reportParameter.Dependencies[j] = parameterInfo.DependencyList[j].Name;
					}
				}
				reportParameter.ValidValuesQueryBasedSpecified = true;
				reportParameter.ValidValuesQueryBased = parameterInfo.DynamicValidValues;
				if (parameterInfo.ValidValues != null)
				{
					reportParameter.ValidValues = new ValidValue[parameterInfo.ValidValues.Count];
					for (int k = 0; k < parameterInfo.ValidValues.Count; k++)
					{
						ValidValue validValue = new ValidValue();
						validValue.Label = parameterInfo.ValidValues[k].Label;
						validValue.Value = parameterInfo.CastToString(parameterInfo.ValidValues[k].Value, Localization.ClientPrimaryCulture);
						reportParameter.ValidValues[k] = validValue;
					}
				}
				reportParameter.DefaultValuesQueryBasedSpecified = true;
				reportParameter.DefaultValuesQueryBased = parameterInfo.DynamicDefaultValue;
				if (parameterInfo.Values != null)
				{
					reportParameter.DefaultValues = new string[parameterInfo.Values.Length];
					for (int l = 0; l < parameterInfo.Values.Length; l++)
					{
						reportParameter.DefaultValues[l] = parameterInfo.CastToString(parameterInfo.Values[l], Localization.ClientPrimaryCulture);
					}
				}
				else if (parameterInfo.DefaultValues != null)
				{
					reportParameter.DefaultValues = new string[parameterInfo.DefaultValues.Length];
					for (int m = 0; m < parameterInfo.DefaultValues.Length; m++)
					{
						reportParameter.DefaultValues[m] = parameterInfo.CastToString(parameterInfo.DefaultValues[m], Localization.ClientPrimaryCulture);
					}
				}
				reportParameter.StateSpecified = true;
				reportParameter.State = ReportParameter.ToSoapParameterStateEnum(parameterInfo.State);
				array[i] = reportParameter;
			}
			return array;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0006F2AD File Offset: 0x0006D4AD
		internal static ParameterStateEnum ToSoapParameterStateEnum(ReportParameterState state)
		{
			switch (state)
			{
			case ReportParameterState.HasValidValue:
				return ParameterStateEnum.HasValidValue;
			case ReportParameterState.InvalidValueProvided:
			case ReportParameterState.DefaultValueInvalid:
			case ReportParameterState.MissingValidValue:
				return ParameterStateEnum.MissingValidValue;
			case ReportParameterState.HasOutstandingDependencies:
				return ParameterStateEnum.HasOutstandingDependencies;
			case ReportParameterState.DynamicValuesUnavailable:
				return ParameterStateEnum.DynamicValuesUnavailable;
			default:
				return ParameterStateEnum.MissingValidValue;
			}
		}

		// Token: 0x04000AC6 RID: 2758
		public string Name;

		// Token: 0x04000AC7 RID: 2759
		public ParameterTypeEnum Type;

		// Token: 0x04000AC8 RID: 2760
		[XmlIgnore]
		public bool TypeSpecified;

		// Token: 0x04000AC9 RID: 2761
		public bool Nullable;

		// Token: 0x04000ACA RID: 2762
		[XmlIgnore]
		public bool NullableSpecified;

		// Token: 0x04000ACB RID: 2763
		public bool AllowBlank;

		// Token: 0x04000ACC RID: 2764
		[XmlIgnore]
		public bool AllowBlankSpecified;

		// Token: 0x04000ACD RID: 2765
		public bool MultiValue;

		// Token: 0x04000ACE RID: 2766
		[XmlIgnore]
		public bool MultiValueSpecified;

		// Token: 0x04000ACF RID: 2767
		public bool QueryParameter;

		// Token: 0x04000AD0 RID: 2768
		[XmlIgnore]
		public bool QueryParameterSpecified;

		// Token: 0x04000AD1 RID: 2769
		public string Prompt;

		// Token: 0x04000AD2 RID: 2770
		public bool PromptUser;

		// Token: 0x04000AD3 RID: 2771
		[XmlIgnore]
		public bool PromptUserSpecified;

		// Token: 0x04000AD4 RID: 2772
		[XmlArrayItem("Dependency")]
		public string[] Dependencies;

		// Token: 0x04000AD5 RID: 2773
		public bool ValidValuesQueryBased;

		// Token: 0x04000AD6 RID: 2774
		[XmlIgnore]
		public bool ValidValuesQueryBasedSpecified;

		// Token: 0x04000AD7 RID: 2775
		public ValidValue[] ValidValues;

		// Token: 0x04000AD8 RID: 2776
		public bool DefaultValuesQueryBased;

		// Token: 0x04000AD9 RID: 2777
		[XmlIgnore]
		public bool DefaultValuesQueryBasedSpecified;

		// Token: 0x04000ADA RID: 2778
		[XmlArrayItem("Value")]
		public string[] DefaultValues;

		// Token: 0x04000ADB RID: 2779
		public ParameterStateEnum State;

		// Token: 0x04000ADC RID: 2780
		[XmlIgnore]
		public bool StateSpecified;

		// Token: 0x04000ADD RID: 2781
		public string ErrorMessage;

		// Token: 0x04000ADE RID: 2782
		private const string ElementName = "ReportParameter";
	}
}
