using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000624 RID: 1572
	[Serializable]
	public sealed class ParameterInfoCollection : ArrayList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, ISerializable
	{
		// Token: 0x06005664 RID: 22116 RVA: 0x0016C0BE File Offset: 0x0016A2BE
		public ParameterInfoCollection()
		{
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x0016C0C6 File Offset: 0x0016A2C6
		internal ParameterInfoCollection(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06005666 RID: 22118 RVA: 0x0016C0CF File Offset: 0x0016A2CF
		public ParameterInfoCollection(SerializationInfo info, StreamingContext context)
		{
			this.PopulateFromXml(info.GetString("Xml"));
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x0016C0E8 File Offset: 0x0016A2E8
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Xml", this.ToXmlWithTransientState());
		}

		// Token: 0x17001F93 RID: 8083
		public ParameterInfo this[int index]
		{
			get
			{
				return (ParameterInfo)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x17001F94 RID: 8084
		public ParameterInfo this[string name]
		{
			get
			{
				for (int i = 0; i < base.Count; i++)
				{
					if (this[i].Name == name)
					{
						return this[i];
					}
				}
				return null;
			}
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x0016C14F File Offset: 0x0016A34F
		public void Add(ParameterInfo parameterInfo)
		{
			base.Add(parameterInfo);
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x0016C15C File Offset: 0x0016A35C
		public void CopyTo(ParameterInfoCollection target)
		{
			if (target == null)
			{
				return;
			}
			for (int i = 0; i < base.Count; i++)
			{
				target.Add(new ParameterInfo(this[i]));
			}
			if (this.ParametersLayout != null)
			{
				target.ParametersLayout = new ParametersGridLayout
				{
					NumberOfColumns = this.ParametersLayout.NumberOfColumns,
					NumberOfRows = this.ParametersLayout.NumberOfRows,
					CellDefinitions = new ParametersGridCellDefinitionList()
				};
				if (this.ParametersLayout.CellDefinitions != null)
				{
					foreach (object obj in this.ParametersLayout.CellDefinitions)
					{
						ParameterGridLayoutCellDefinition parameterGridLayoutCellDefinition = (ParameterGridLayoutCellDefinition)obj;
						target.ParametersLayout.CellDefinitions.Add(new ParameterGridLayoutCellDefinition
						{
							ColumnIndex = parameterGridLayoutCellDefinition.ColumnIndex,
							RowIndex = parameterGridLayoutCellDefinition.RowIndex,
							ParameterName = parameterGridLayoutCellDefinition.ParameterName
						});
					}
				}
			}
			target.FixupDependencies();
		}

		// Token: 0x17001F95 RID: 8085
		// (get) Token: 0x0600566D RID: 22125 RVA: 0x0016C26C File Offset: 0x0016A46C
		public NameValueCollection AsNameValueCollectionInUserCulture
		{
			get
			{
				NameValueCollection nameValueCollection = new NameValueCollection(this.Count, StringComparer.Ordinal);
				foreach (object obj in this)
				{
					ParameterInfo parameterInfo = (ParameterInfo)obj;
					if (parameterInfo.Values != null && parameterInfo.Values.Length != 0)
					{
						for (int i = 0; i < parameterInfo.Values.Length; i++)
						{
							nameValueCollection.Add(parameterInfo.Name, parameterInfo.CastToString(parameterInfo.Values[i], Localization.ClientPrimaryCulture));
						}
					}
				}
				return nameValueCollection;
			}
		}

		// Token: 0x17001F96 RID: 8086
		// (get) Token: 0x0600566E RID: 22126 RVA: 0x0016C314 File Offset: 0x0016A514
		public int VisibleCount
		{
			get
			{
				int num = 0;
				using (IEnumerator enumerator = this.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((ParameterInfo)enumerator.Current).IsVisible)
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x0600566F RID: 22127 RVA: 0x0016C370 File Offset: 0x0016A570
		public string GetParameterWithNoValue()
		{
			for (int i = 0; i < this.Count; i++)
			{
				ParameterInfo parameterInfo = this[i];
				if (!parameterInfo.DynamicDefaultValue && parameterInfo.Values == null && parameterInfo.DefaultValues == null)
				{
					return parameterInfo.Name;
				}
			}
			return null;
		}

		// Token: 0x06005670 RID: 22128 RVA: 0x0016C3B8 File Offset: 0x0016A5B8
		public bool ValuesAreValid(out bool satisfiable, bool throwOnUnsatisfiable, out bool hasMissingValidValue)
		{
			hasMissingValidValue = false;
			if (!this.Validated)
			{
				for (int i = 0; i < this.Count; i++)
				{
					ParameterInfo parameterInfo = this[i];
					if (parameterInfo.DynamicValidValues)
					{
						parameterInfo.State = ReportParameterState.DynamicValuesUnavailable;
					}
					else if (parameterInfo.ValueIsValid())
					{
						parameterInfo.State = ReportParameterState.HasValidValue;
					}
					else
					{
						hasMissingValidValue = true;
						parameterInfo.State = ReportParameterState.MissingValidValue;
					}
				}
			}
			bool flag = true;
			for (int j = 0; j < this.Count; j++)
			{
				ParameterInfo parameterInfo2 = this[j];
				if (parameterInfo2.State != ReportParameterState.HasValidValue)
				{
					flag = false;
					if (!parameterInfo2.PromptUser && parameterInfo2.State == ReportParameterState.MissingValidValue)
					{
						hasMissingValidValue = true;
						satisfiable = false;
						if (throwOnUnsatisfiable)
						{
							throw new ReportProcessingException(ErrorCode.rsParameterError);
						}
						return false;
					}
				}
			}
			satisfiable = true;
			return flag;
		}

		// Token: 0x06005671 RID: 22129 RVA: 0x0016C46C File Offset: 0x0016A66C
		public bool ValuesAreValid()
		{
			bool flag;
			bool flag2;
			return this.ValuesAreValid(out flag, false, out flag2);
		}

		// Token: 0x06005672 RID: 22130 RVA: 0x0016C484 File Offset: 0x0016A684
		public bool ValuesAreValid(out bool hasMissingValidValue)
		{
			bool flag;
			return this.ValuesAreValid(out flag, false, out hasMissingValidValue);
		}

		// Token: 0x06005673 RID: 22131 RVA: 0x0016C49C File Offset: 0x0016A69C
		public bool ValuesAreValid(out bool satisfiable, bool throwOnUnsatisfiable)
		{
			bool flag;
			return this.ValuesAreValid(out satisfiable, throwOnUnsatisfiable, out flag);
		}

		// Token: 0x06005674 RID: 22132 RVA: 0x0016C4B4 File Offset: 0x0016A6B4
		public bool NeedPrompts()
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].State != ReportParameterState.HasValidValue)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005675 RID: 22133 RVA: 0x0016C4E4 File Offset: 0x0016A6E4
		public void ThrowIfNotValid()
		{
			for (int i = 0; i < this.Count; i++)
			{
				ParameterInfo parameterInfo = this[i];
				switch (parameterInfo.State)
				{
				case ReportParameterState.HasValidValue:
					break;
				case ReportParameterState.InvalidValueProvided:
				case ReportParameterState.DefaultValueInvalid:
					throw new InvalidReportParameterException(parameterInfo.Name);
				case ReportParameterState.MissingValidValue:
				{
					bool flag = ParameterBase.IsSharedDataSetParameterObjectType(parameterInfo.ParameterObjectType);
					ParameterInfoCollection.ThrowParameterValueNotSetException(parameterInfo.Name, flag);
					break;
				}
				case ReportParameterState.HasOutstandingDependencies:
				case ReportParameterState.DynamicValuesUnavailable:
					throw new ReportProcessingException(ErrorCode.rsReportParameterProcessingError, new object[] { parameterInfo.Name });
				default:
					throw new InternalCatalogException("Invalid parameter state encountered");
				}
			}
		}

		// Token: 0x06005676 RID: 22134 RVA: 0x0016C57C File Offset: 0x0016A77C
		public void ValidateInputValues(ParamValues inputValues, bool isSnapshotExecution, bool isSharedDataSetParameters)
		{
			object obj = new object();
			for (int i = 0; i < this.Count; i++)
			{
				ParameterInfo parameterInfo = this[i];
				ParamValueList paramValueList = inputValues[parameterInfo.Name];
				bool flag = isSnapshotExecution && parameterInfo.UsedInQuery && parameterInfo.DefaultValues != null && parameterInfo.DefaultValues.Length != 0;
				Dictionary<object, bool> dictionary = null;
				if (flag)
				{
					dictionary = new Dictionary<object, bool>(parameterInfo.DefaultValues.Length);
					for (int j = 0; j < parameterInfo.DefaultValues.Length; j++)
					{
						object obj2 = parameterInfo.DefaultValues[j];
						if (obj2 == null)
						{
							obj2 = obj;
						}
						if (!dictionary.ContainsKey(obj2))
						{
							dictionary.Add(obj2, false);
						}
					}
				}
				if (paramValueList == null)
				{
					if (parameterInfo.PromptUser && parameterInfo.DefaultValues == null && !parameterInfo.Nullable)
					{
						if (isSharedDataSetParameters)
						{
							Global.Tracer.Assert(ParameterBase.IsSharedDataSetParameterObjectType(parameterInfo.ParameterObjectType), "param.IsSharedDataSetParameter");
						}
						ParameterInfoCollection.ThrowParameterValueNotSetException(parameterInfo.Name, isSharedDataSetParameters);
					}
				}
				else
				{
					for (int k = 0; k < paramValueList.Count; k++)
					{
						ParamValue paramValue = paramValueList[k];
						if (!paramValue.UseField)
						{
							object obj3 = null;
							string text = paramValue.Value;
							if (text != null && text.Length == 0 && parameterInfo.DataType != DataType.String)
							{
								text = null;
							}
							if (text != null && parameterInfo.DataType != DataType.Object && !ParameterBase.CastFromString(text, out obj3, parameterInfo.DataType, Localization.ClientPrimaryCulture))
							{
								throw new ReportParameterTypeMismatchException(paramValue.Name);
							}
							if (flag)
							{
								object obj4 = obj3;
								if (obj3 == null)
								{
									obj3 = obj;
								}
								if (!dictionary.ContainsKey(obj4))
								{
									throw new InvalidReportParameterException(paramValue.Name);
								}
								dictionary[obj4] = true;
							}
							else
							{
								if (text == null)
								{
									if (parameterInfo.Nullable)
									{
										goto IL_0264;
									}
									if (isSharedDataSetParameters)
									{
										Global.Tracer.Assert(ParameterBase.IsSharedDataSetParameterObjectType(parameterInfo.ParameterObjectType), "fixedInputValu == null -> param.IsSharedDataSetParameter");
									}
									ParameterInfoCollection.ThrowParameterValueNotSetException(parameterInfo.Name, isSharedDataSetParameters);
								}
								else if (parameterInfo.DataType == DataType.String && text == string.Empty)
								{
									if (!parameterInfo.AllowBlank)
									{
										throw new InvalidReportParameterException(parameterInfo.Name);
									}
									goto IL_0264;
								}
								if (parameterInfo.ValidValues != null)
								{
									bool flag2 = false;
									int count = parameterInfo.ValidValues.Count;
									for (int l = 0; l < count; l++)
									{
										if (ParameterBase.ParameterValuesEqual(parameterInfo.ValidValues[l].Value, obj3))
										{
											flag2 = true;
											break;
										}
									}
									if (!flag2 && parameterInfo.ValidValues.Count > 0)
									{
										throw new InvalidReportParameterException(paramValue.Name);
									}
								}
							}
						}
						IL_0264:;
					}
					if (flag)
					{
						foreach (KeyValuePair<object, bool> keyValuePair in dictionary)
						{
							if (!keyValuePair.Value)
							{
								throw new InvalidReportParameterException(parameterInfo.Name);
							}
						}
					}
				}
			}
			if (inputValues != null)
			{
				foreach (object obj5 in inputValues.Values)
				{
					string name = ((ParamValueList)obj5)[0].Name;
					if (this[name] == null)
					{
						ParameterInfoCollection.ThrowUnknownParameterException(name, isSharedDataSetParameters);
					}
				}
			}
		}

		// Token: 0x06005677 RID: 22135 RVA: 0x0016C8D0 File Offset: 0x0016AAD0
		private static void ThrowParameterValueNotSetException(string name, bool isSharedDataSetParameter)
		{
			if (isSharedDataSetParameter)
			{
				throw new DataSetParameterValueNotSetException(name);
			}
			throw new ReportParameterValueNotSetException(name);
		}

		// Token: 0x06005678 RID: 22136 RVA: 0x0016C8E2 File Offset: 0x0016AAE2
		private static void ThrowUnknownParameterException(string name, bool isSharedDataSetParameter)
		{
			if (isSharedDataSetParameter)
			{
				throw new UnknownDataSetParameterException(name);
			}
			throw new UnknownReportParameterException(name);
		}

		// Token: 0x06005679 RID: 22137 RVA: 0x0016C8F4 File Offset: 0x0016AAF4
		private static void ThrowReadOnlyParameterException(string name, bool isSharedDataSetParameter)
		{
			if (isSharedDataSetParameter)
			{
				throw new ReadOnlyDataSetParameterException(name);
			}
			throw new ReadOnlyReportParameterException(name);
		}

		// Token: 0x0600567A RID: 22138 RVA: 0x0016C906 File Offset: 0x0016AB06
		public string ToUrl(bool skipInternalParameters)
		{
			return this.ToUrl(skipInternalParameters, null);
		}

		// Token: 0x0600567B RID: 22139 RVA: 0x0016C910 File Offset: 0x0016AB10
		public string ToUrl(bool skipInternalParameters, Func<object, string> cs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this)
			{
				ParameterInfo parameterInfo = (ParameterInfo)obj;
				if (!skipInternalParameters || parameterInfo.PromptUser)
				{
					if (parameterInfo.Values != null)
					{
						foreach (object obj2 in parameterInfo.Values)
						{
							ParameterInfoCollection.UrlEncodeSingleParam(stringBuilder, parameterInfo.Name, obj2, cs);
						}
					}
					else
					{
						ParameterInfoCollection.UrlEncodeSingleParam(stringBuilder, parameterInfo.Name, null, cs);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600567C RID: 22140 RVA: 0x0016C9C0 File Offset: 0x0016ABC0
		private static string UrlEncodeString(string param)
		{
			return UrlUtil.UrlEncode(param).Replace("'", "%27");
		}

		// Token: 0x0600567D RID: 22141 RVA: 0x0016C9D7 File Offset: 0x0016ABD7
		private static void UrlEncodeSingleParam(StringBuilder url, string name, object val)
		{
			ParameterInfoCollection.UrlEncodeSingleParam(url, name, val, null);
		}

		// Token: 0x0600567E RID: 22142 RVA: 0x0016C9E4 File Offset: 0x0016ABE4
		private static void UrlEncodeSingleParam(StringBuilder url, string name, object val, Func<object, string> cs)
		{
			if (url.Length > 0)
			{
				url.Append('&');
			}
			url.Append(ParameterInfoCollection.UrlEncodeString(name));
			if (val == null)
			{
				url.Append(":isnull=true");
				return;
			}
			url.Append('=');
			try
			{
				url.Append(ParameterInfoCollection.UrlEncodeString((cs == null) ? val.ToString() : cs(val)));
			}
			catch (UriFormatException ex)
			{
				throw new InvalidParameterException(name, ex);
			}
		}

		// Token: 0x0600567F RID: 22143 RVA: 0x0016CA64 File Offset: 0x0016AC64
		public static string ToUrl(NameValueCollection coll)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < coll.Count; i++)
			{
				string key = coll.GetKey(i);
				string[] values = coll.GetValues(i);
				if (values == null)
				{
					ParameterInfoCollection.UrlEncodeSingleParam(stringBuilder, key, null);
				}
				else
				{
					foreach (string text in values)
					{
						ParameterInfoCollection.UrlEncodeSingleParam(stringBuilder, key, text);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005680 RID: 22144 RVA: 0x0016CAD1 File Offset: 0x0016ACD1
		public string ToXmlWithTransientState()
		{
			return this.ToXml(false, true, false);
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x0016CADC File Offset: 0x0016ACDC
		public string ToXml(bool usedInQueryValuesOnly)
		{
			return this.ToXml(usedInQueryValuesOnly, false, usedInQueryValuesOnly);
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x0016CAE8 File Offset: 0x0016ACE8
		private string ToXml(bool usedInQueryValuesOnly, bool writeTransientState, bool convertToString)
		{
			XmlTextWriter xmlTextWriter = null;
			string text2;
			try
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				xmlTextWriter = new XmlTextWriter(stringWriter);
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartElement("Parameters");
				XmlWriter xmlWriter = xmlTextWriter;
				string text = "UserProfileState";
				int userProfileState = (int)this.m_userProfileState;
				xmlWriter.WriteElementString(text, userProfileState.ToString(CultureInfo.InvariantCulture));
				for (int i = 0; i < this.Count; i++)
				{
					ParameterInfo parameterInfo = this[i];
					if (usedInQueryValuesOnly)
					{
						if (parameterInfo.UsedInQuery)
						{
							parameterInfo.WriteNameValueToXml(xmlTextWriter, convertToString);
						}
					}
					else
					{
						parameterInfo.WriteToXml(xmlTextWriter, writeTransientState);
					}
				}
				if (!usedInQueryValuesOnly)
				{
					this.WriteParametersLayoutXml(xmlTextWriter);
				}
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.Flush();
				text2 = stringWriter.ToString();
			}
			finally
			{
				if (xmlTextWriter != null)
				{
					xmlTextWriter.Close();
				}
			}
			return text2;
		}

		// Token: 0x06005683 RID: 22147 RVA: 0x0016CBB0 File Offset: 0x0016ADB0
		private void WriteParametersLayoutXml(XmlTextWriter resultXml)
		{
			if (this.m_parametersLayout != null)
			{
				resultXml.WriteStartElement("ParametersLayout");
				resultXml.WriteStartElement("ParametersGridLayoutDefinition");
				resultXml.WriteElementString("NumberOfColumns", this.m_parametersLayout.NumberOfColumns.ToString(CultureInfo.InvariantCulture));
				resultXml.WriteElementString("NumberOfRows", this.m_parametersLayout.NumberOfRows.ToString(CultureInfo.InvariantCulture));
				if (this.m_parametersLayout.CellDefinitions != null && this.m_parametersLayout.CellDefinitions.Count > 0)
				{
					resultXml.WriteStartElement("CellDefinitions");
					foreach (object obj in this.m_parametersLayout.CellDefinitions)
					{
						(obj as ParameterGridLayoutCellDefinition).WriteXml(resultXml);
					}
					resultXml.WriteEndElement();
				}
				resultXml.WriteEndElement();
				resultXml.WriteEndElement();
			}
		}

		// Token: 0x06005684 RID: 22148 RVA: 0x0016CCAC File Offset: 0x0016AEAC
		public static ParameterInfoCollection DecodeFromXml(string paramString)
		{
			ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection();
			parameterInfoCollection.PopulateFromXml(paramString);
			return parameterInfoCollection;
		}

		// Token: 0x06005685 RID: 22149 RVA: 0x0016CCBC File Offset: 0x0016AEBC
		private void PopulateFromXml(string paramString)
		{
			if (paramString == null || paramString == string.Empty)
			{
				return;
			}
			XmlReader xmlReader = XmlUtil.SafeCreateXmlTextReader(paramString);
			try
			{
				xmlReader.MoveToContent();
				if (xmlReader.NodeType != XmlNodeType.Element || xmlReader.Name != "Parameters")
				{
					throw new InvalidXmlException();
				}
				while (xmlReader.Read())
				{
					if (xmlReader.IsStartElement())
					{
						if (xmlReader.IsEmptyElement)
						{
							throw new InvalidXmlException();
						}
						string name = xmlReader.Name;
						if (!(name == "Parameter"))
						{
							if (!(name == "UserProfileState"))
							{
								if (!(name == "ParametersLayout"))
								{
									throw new InvalidXmlException();
								}
								xmlReader.Read();
								ParameterInfoCollection.ParseParametersLayout(xmlReader, this, CultureInfo.InvariantCulture);
							}
							else
							{
								string text = xmlReader.ReadString();
								this.UserProfileState = (UserProfileState)int.Parse(text, CultureInfo.InvariantCulture);
							}
						}
						else
						{
							xmlReader.Read();
							ParameterInfoCollection.ParseOneParameter(xmlReader, this, CultureInfo.InvariantCulture);
						}
					}
				}
			}
			catch (XmlException ex)
			{
				throw new MalformedXmlException(ex);
			}
		}

		// Token: 0x06005686 RID: 22150 RVA: 0x0016CDC4 File Offset: 0x0016AFC4
		public static ParameterInfoCollection DecodeFromNameValueCollectionAndUserCulture(NameValueCollection collection)
		{
			return ParameterInfoCollection.DecodeFromNameValueCollectionAndUserCulture(collection, false);
		}

		// Token: 0x06005687 RID: 22151 RVA: 0x0016CDD0 File Offset: 0x0016AFD0
		public static ParameterInfoCollection DecodeFromNameValueCollectionAndUserCulture(NameValueCollection collection, bool isDataSetParameters)
		{
			ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection();
			if (collection == null || collection.Count == 0)
			{
				return parameterInfoCollection;
			}
			for (int i = 0; i < collection.Count; i++)
			{
				ParameterInfo parameterInfo = new ParameterInfo();
				List<string> list = new List<string>();
				string[] values = collection.GetValues(i);
				if (values == null)
				{
					list.Add(null);
				}
				else
				{
					for (int j = 0; j < values.Length; j++)
					{
						list.Add(values[j]);
					}
				}
				parameterInfo.Parse(collection.GetKey(i), null, null, null, null, null, null, null, null, null, null, null, null, null, null, list, null, Localization.ClientPrimaryCulture, null);
				if (isDataSetParameters)
				{
					parameterInfo.DataType = DataType.Object;
				}
				parameterInfoCollection.Add(parameterInfo);
			}
			return parameterInfoCollection;
		}

		// Token: 0x06005688 RID: 22152 RVA: 0x0016CE7C File Offset: 0x0016B07C
		private static void ParseOneParameter(XmlReader sourceXmlReader, ParameterInfoCollection result, CultureInfo culture)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string text6 = null;
			string text7 = null;
			string text8 = null;
			string text9 = null;
			ValidValueList validValueList = null;
			List<string> list = null;
			List<string> list2 = null;
			List<string> list3 = null;
			string text10 = null;
			string text11 = null;
			string text12 = null;
			string text13 = null;
			string text14 = null;
			string text15 = null;
			string text16 = null;
			while (sourceXmlReader.IsStartElement())
			{
				bool isEmptyElement = sourceXmlReader.IsEmptyElement;
				string name = sourceXmlReader.Name;
				string text17 = sourceXmlReader.ReadString();
				if (name != null)
				{
					switch (name.Length)
					{
					case 4:
					{
						char c = name[0];
						if (c != 'N')
						{
							if (c == 'T')
							{
								if (name == "Type")
								{
									text2 = text17;
								}
							}
						}
						else if (name == "Name")
						{
							text = text17;
						}
						break;
					}
					case 5:
						if (name == "State")
						{
							text9 = text17;
						}
						break;
					case 6:
					{
						char c = name[0];
						if (c != 'P')
						{
							if (c == 'V')
							{
								if (name == "Values")
								{
									if (!isEmptyElement)
									{
										list2 = ParameterInfoCollection.ParseXmlList(sourceXmlReader, "Value");
									}
								}
							}
						}
						else if (name == "Prompt")
						{
							text4 = text17;
						}
						break;
					}
					case 8:
						if (name == "Nullable")
						{
							text3 = text17;
						}
						break;
					case 10:
					{
						char c = name[0];
						if (c != 'A')
						{
							if (c != 'M')
							{
								if (c == 'P')
								{
									if (name == "PromptUser")
									{
										text7 = text17;
									}
								}
							}
							else if (name == "MultiValue")
							{
								text6 = text17;
							}
						}
						else if (name == "AllowBlank")
						{
							text5 = text17;
						}
						break;
					}
					case 11:
					{
						char c = name[0];
						if (c != 'U')
						{
							if (c == 'V')
							{
								if (name == "ValidValues")
								{
									if (!isEmptyElement)
									{
										validValueList = ParameterInfoCollection.ParseValidValues(sourceXmlReader);
									}
								}
							}
						}
						else if (name == "UsedInQuery")
						{
							text8 = text17;
						}
						break;
					}
					case 12:
						if (name == "Dependencies")
						{
							if (!isEmptyElement)
							{
								list3 = ParameterInfoCollection.ParseXmlList(sourceXmlReader, "Dependency");
							}
						}
						break;
					case 13:
					{
						char c = name[1];
						if (c != 'a')
						{
							if (c != 'e')
							{
								if (c == 'y')
								{
									if (name == "DynamicPrompt")
									{
										text12 = text17;
									}
								}
							}
							else if (name == "DefaultValues")
							{
								if (!isEmptyElement)
								{
									list = ParameterInfoCollection.ParseXmlList(sourceXmlReader, "Value");
								}
							}
						}
						else if (name == "ValuesChanged")
						{
							if (!isEmptyElement)
							{
								text13 = text17;
							}
						}
						break;
					}
					case 14:
						if (name == "IsUserSupplied")
						{
							if (!isEmptyElement)
							{
								text15 = text17;
							}
						}
						break;
					case 17:
						if (name == "UseAllValidValues")
						{
							if (!isEmptyElement)
							{
								text16 = text17;
							}
						}
						break;
					case 18:
						if (name == "DynamicValidValues")
						{
							text10 = text17;
						}
						break;
					case 19:
						if (name == "DynamicDefaultValue")
						{
							text11 = text17;
						}
						break;
					case 23:
						if (name == "UseExplicitDefaultValue")
						{
							if (!isEmptyElement)
							{
								text14 = text17;
							}
						}
						break;
					}
				}
				if (!isEmptyElement)
				{
					sourceXmlReader.ReadEndElement();
				}
				else
				{
					sourceXmlReader.Read();
				}
			}
			ParameterInfo parameterInfo = new ParameterInfo();
			ParameterInfoCollection parameterInfoCollection = ParameterInfoCollection.ParseDependencies(result, list3);
			parameterInfo.Parse(text, text2, text3, text5, text6, text8, text9, text12, text4, text7, parameterInfoCollection, text10, validValueList, text11, list, list2, null, culture, text16);
			bool flag;
			if (text15 != null && bool.TryParse(text15, out flag))
			{
				parameterInfo.IsUserSupplied = flag;
			}
			if (text13 != null && bool.TryParse(text13, out flag))
			{
				parameterInfo.ValuesChanged = flag;
			}
			if (text14 != null && bool.TryParse(text14, out flag))
			{
				parameterInfo.UseExplicitDefaultValue = flag;
			}
			try
			{
				result.Add(parameterInfo);
			}
			catch (ArgumentException)
			{
				throw new InvalidXmlException();
			}
		}

		// Token: 0x06005689 RID: 22153 RVA: 0x0016D348 File Offset: 0x0016B548
		private static void ParseParametersLayout(XmlReader sourceXmlReader, ParameterInfoCollection result, CultureInfo culture)
		{
			while (sourceXmlReader.IsStartElement())
			{
				bool isEmptyElement = sourceXmlReader.IsEmptyElement;
				string name = sourceXmlReader.Name;
				sourceXmlReader.ReadString();
				if (name == "ParametersGridLayoutDefinition")
				{
					ParameterInfoCollection.ParseParametersGridLayoutDefinition(sourceXmlReader, result, culture);
					sourceXmlReader.ReadEndElement();
				}
				sourceXmlReader.ReadEndElement();
			}
		}

		// Token: 0x0600568A RID: 22154 RVA: 0x0016D388 File Offset: 0x0016B588
		private static void ParseParametersGridLayoutDefinition(XmlReader sourceXmlReader, ParameterInfoCollection result, CultureInfo culture)
		{
			ParametersGridLayout parametersGridLayout = new ParametersGridLayout();
			parametersGridLayout.CellDefinitions = new ParametersGridCellDefinitionList();
			while (sourceXmlReader.IsStartElement())
			{
				bool isEmptyElement = sourceXmlReader.IsEmptyElement;
				string name = sourceXmlReader.Name;
				string text = sourceXmlReader.ReadString();
				if (!(name == "NumberOfColumns"))
				{
					if (!(name == "NumberOfRows"))
					{
						if (name == "CellDefinitions")
						{
							ParameterInfoCollection.ParseParametersCellDefinitions(parametersGridLayout, sourceXmlReader, result, culture);
						}
					}
					else
					{
						parametersGridLayout.NumberOfRows = int.Parse(text, CultureInfo.InvariantCulture);
					}
				}
				else
				{
					parametersGridLayout.NumberOfColumns = int.Parse(text, CultureInfo.InvariantCulture);
				}
				sourceXmlReader.ReadEndElement();
			}
			result.ParametersLayout = parametersGridLayout;
		}

		// Token: 0x0600568B RID: 22155 RVA: 0x0016D42C File Offset: 0x0016B62C
		private static void ParseParametersCellDefinitions(ParametersGridLayout gridLayout, XmlReader sourceXmlReader, ParameterInfoCollection result, CultureInfo culture)
		{
			gridLayout.CellDefinitions = new ParametersGridCellDefinitionList();
			while (sourceXmlReader.IsStartElement())
			{
				bool isEmptyElement = sourceXmlReader.IsEmptyElement;
				string name = sourceXmlReader.Name;
				sourceXmlReader.ReadString();
				if (name == "CellDefinition")
				{
					ParameterGridLayoutCellDefinition parameterGridLayoutCellDefinition = ParameterInfoCollection.ParseParameterCellDefinition(sourceXmlReader, result, culture);
					gridLayout.CellDefinitions.Add(parameterGridLayoutCellDefinition);
					sourceXmlReader.ReadEndElement();
				}
			}
		}

		// Token: 0x0600568C RID: 22156 RVA: 0x0016D48C File Offset: 0x0016B68C
		private static ParameterGridLayoutCellDefinition ParseParameterCellDefinition(XmlReader sourceXmlReader, ParameterInfoCollection result, CultureInfo culture)
		{
			ParameterGridLayoutCellDefinition parameterGridLayoutCellDefinition = new ParameterGridLayoutCellDefinition();
			while (sourceXmlReader.IsStartElement())
			{
				bool isEmptyElement = sourceXmlReader.IsEmptyElement;
				string name = sourceXmlReader.Name;
				string text = sourceXmlReader.ReadString();
				if (!(name == "RowIndex"))
				{
					if (!(name == "ColumnIndex"))
					{
						if (name == "ParameterName")
						{
							parameterGridLayoutCellDefinition.ParameterName = text;
						}
					}
					else
					{
						parameterGridLayoutCellDefinition.ColumnIndex = int.Parse(text, CultureInfo.InvariantCulture);
					}
				}
				else
				{
					parameterGridLayoutCellDefinition.RowIndex = int.Parse(text, CultureInfo.InvariantCulture);
				}
				sourceXmlReader.ReadEndElement();
			}
			return parameterGridLayoutCellDefinition;
		}

		// Token: 0x0600568D RID: 22157 RVA: 0x0016D51C File Offset: 0x0016B71C
		private static ParameterInfoCollection ParseDependencies(ParameterInfoCollection parameters, List<string> dependencies)
		{
			if (dependencies == null)
			{
				return null;
			}
			ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection();
			for (int i = 0; i < dependencies.Count; i++)
			{
				ParameterInfo parameterInfo = parameters[dependencies[i]];
				if (parameterInfo == null)
				{
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "Found that parameter '{0}' depend on parameter that is not found before it.", dependencies[i]));
				}
				parameterInfoCollection.Add(parameterInfo);
			}
			return parameterInfoCollection;
		}

		// Token: 0x0600568E RID: 22158 RVA: 0x0016D57C File Offset: 0x0016B77C
		private static List<string> ParseXmlList(XmlReader sourceXmlReader, string expectedElement)
		{
			List<string> list = null;
			while (sourceXmlReader.IsStartElement())
			{
				bool isEmptyElement = sourceXmlReader.IsEmptyElement;
				string name = sourceXmlReader.Name;
				string attribute = sourceXmlReader.GetAttribute("nil");
				string text = sourceXmlReader.ReadString();
				if (expectedElement != null && name != expectedElement)
				{
					throw new InvalidXmlException();
				}
				if (list == null)
				{
					list = new List<string>();
				}
				if (attribute == bool.TrueString)
				{
					list.Add(null);
				}
				else
				{
					list.Add(text);
				}
				if (!isEmptyElement)
				{
					sourceXmlReader.ReadEndElement();
				}
				else
				{
					sourceXmlReader.Read();
				}
			}
			return list;
		}

		// Token: 0x0600568F RID: 22159 RVA: 0x0016D600 File Offset: 0x0016B800
		private static ValidValueList ParseValidValues(XmlReader sourceXmlReader)
		{
			ValidValueList validValueList = null;
			while (sourceXmlReader.IsStartElement())
			{
				bool isEmptyElement = sourceXmlReader.IsEmptyElement;
				string name = sourceXmlReader.Name;
				sourceXmlReader.ReadString();
				if (name != "ValidValue")
				{
					throw new InvalidXmlException();
				}
				if (validValueList == null)
				{
					validValueList = new ValidValueList();
				}
				string text = null;
				string text2 = null;
				if (!isEmptyElement)
				{
					ParameterInfoCollection.ParseValueLabel(sourceXmlReader, out text, out text2);
				}
				ValidValue validValue = new ValidValue(text, text2);
				validValueList.Add(validValue);
				if (!isEmptyElement)
				{
					sourceXmlReader.ReadEndElement();
				}
				else
				{
					sourceXmlReader.Read();
				}
			}
			return validValueList;
		}

		// Token: 0x06005690 RID: 22160 RVA: 0x0016D67C File Offset: 0x0016B87C
		private static void ParseValueLabel(XmlReader sourceXmlReader, out string val, out string label)
		{
			val = null;
			label = null;
			while (sourceXmlReader.IsStartElement())
			{
				bool isEmptyElement = sourceXmlReader.IsEmptyElement;
				string name = sourceXmlReader.Name;
				string text = sourceXmlReader.ReadString();
				if (name == "Value")
				{
					val = text;
				}
				else if (name == "Label")
				{
					label = text;
				}
				if (!isEmptyElement)
				{
					sourceXmlReader.ReadEndElement();
				}
				else
				{
					sourceXmlReader.Read();
				}
			}
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x0016D6E0 File Offset: 0x0016B8E0
		public static ParameterInfoCollection Match(ParameterInfoCollection oldParameters, ParameterInfoCollection newParameters)
		{
			bool flag;
			return ParameterInfoCollection.Match(oldParameters, newParameters, out flag);
		}

		// Token: 0x06005692 RID: 22162 RVA: 0x0016D6F8 File Offset: 0x0016B8F8
		public static ParameterInfoCollection Match(ParameterInfoCollection oldParameters, ParameterInfoCollection newParameters, out bool metaChanges)
		{
			metaChanges = false;
			ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection();
			for (int i = 0; i < newParameters.Count; i++)
			{
				ParameterInfo parameterInfo = newParameters[i];
				ParameterInfo parameterInfo2 = oldParameters[parameterInfo.Name];
				if (parameterInfo2 != null)
				{
					parameterInfo.PromptUser = parameterInfo2.PromptUser;
					if (parameterInfo.DynamicDefaultValue)
					{
						parameterInfoCollection.Add(parameterInfo);
					}
					else
					{
						ParameterInfo parameterInfo3 = ParameterInfo.Cast(parameterInfo2, parameterInfo, Localization.ClientPrimaryCulture, ref metaChanges);
						if (parameterInfo3 != null)
						{
							parameterInfoCollection.Add(parameterInfo3);
						}
						else
						{
							parameterInfoCollection.Add(parameterInfo);
						}
					}
				}
				else
				{
					parameterInfoCollection.Add(parameterInfo);
					metaChanges = true;
				}
			}
			for (int j = 0; j < oldParameters.Count; j++)
			{
				ParameterInfo parameterInfo4 = oldParameters[j];
				ParameterInfo parameterInfo5 = newParameters[parameterInfo4.Name];
				if (parameterInfo5 == null || j != newParameters.IndexOf(parameterInfo5))
				{
					metaChanges = true;
					break;
				}
			}
			parameterInfoCollection.ParametersLayout = newParameters.ParametersLayout;
			return parameterInfoCollection;
		}

		// Token: 0x06005693 RID: 22163 RVA: 0x0016D7D4 File Offset: 0x0016B9D4
		public static ParameterInfoCollection Combine(ParameterInfoCollection oldParameters, ParameterInfoCollection newParameters, bool checkReadOnly, bool ignoreNewQueryParams, bool isParameterDefinitionUpdate, bool isSharedDataSetParameter)
		{
			return ParameterInfoCollection.Combine(oldParameters, newParameters, checkReadOnly, ignoreNewQueryParams, isParameterDefinitionUpdate, isSharedDataSetParameter, Localization.ClientPrimaryCulture);
		}

		// Token: 0x06005694 RID: 22164 RVA: 0x0016D7E8 File Offset: 0x0016B9E8
		public static ParameterInfoCollection Combine(ParameterInfoCollection oldParameters, ParameterInfoCollection newParameters, bool checkReadOnly, bool ignoreNewQueryParams, bool isParameterDefinitionUpdate, bool isSharedDataSetParameter, CultureInfo culture)
		{
			if (newParameters == null)
			{
				return oldParameters;
			}
			ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection();
			for (int i = 0; i < oldParameters.Count; i++)
			{
				ParameterInfo parameterInfo = oldParameters[i];
				ParameterInfo parameterInfo2 = newParameters[parameterInfo.Name];
				if (parameterInfo2 == null || (ignoreNewQueryParams && parameterInfo.UsedInQuery))
				{
					parameterInfo.ValuesChanged = false;
					parameterInfoCollection.Add(parameterInfo);
				}
				else
				{
					if (checkReadOnly && !parameterInfo.PromptUser)
					{
						ParameterInfoCollection.ThrowReadOnlyParameterException(parameterInfo.Name, isSharedDataSetParameter);
					}
					ParameterInfo parameterInfo3 = ParameterInfo.Cast(parameterInfo2, parameterInfo, culture);
					if (parameterInfo3 == null)
					{
						throw new ReportParameterTypeMismatchException(parameterInfo2.Name);
					}
					if (!checkReadOnly)
					{
						parameterInfo3.PromptUser = parameterInfo2.PromptUser;
						parameterInfo3.Prompt = parameterInfo2.Prompt;
					}
					parameterInfo3.IsUserSupplied = true;
					if (isParameterDefinitionUpdate)
					{
						if (parameterInfo2.UseExplicitDefaultValue)
						{
							parameterInfo3.DynamicDefaultValue = parameterInfo2.DynamicDefaultValue;
						}
						else
						{
							Global.Tracer.Assert(parameterInfo3.Values == null && parameterInfo3.DefaultValues == null, "(null == casted.Values && null == casted.DefaultValues)");
							parameterInfo3.Values = parameterInfo.Values;
							parameterInfo3.DefaultValues = parameterInfo.DefaultValues;
						}
					}
					parameterInfo3.ValuesChanged = !ParameterInfoCollection.SameParameterValues(parameterInfo3, parameterInfo);
					parameterInfoCollection.Add(parameterInfo3);
				}
			}
			for (int j = 0; j < newParameters.Count; j++)
			{
				ParameterInfo parameterInfo4 = newParameters[j];
				if (oldParameters[parameterInfo4.Name] == null)
				{
					ParameterInfoCollection.ThrowUnknownParameterException(parameterInfo4.Name, isSharedDataSetParameter);
				}
			}
			parameterInfoCollection.FixupDependencies();
			parameterInfoCollection.UserProfileState = oldParameters.UserProfileState | newParameters.UserProfileState;
			parameterInfoCollection.ParametersLayout = oldParameters.ParametersLayout;
			return parameterInfoCollection;
		}

		// Token: 0x06005695 RID: 22165 RVA: 0x0016D980 File Offset: 0x0016BB80
		private void FixupDependencies()
		{
			for (int i = 0; i < this.Count; i++)
			{
				ParameterInfo parameterInfo = this[i];
				if (parameterInfo.DependencyList != null)
				{
					for (int j = 0; j < parameterInfo.DependencyList.Count; j++)
					{
						ParameterInfo parameterInfo2 = this[parameterInfo.DependencyList[j].Name];
						parameterInfo.DependencyList[j] = parameterInfo2;
						parameterInfo2.OthersDependOnMe = true;
					}
				}
			}
		}

		// Token: 0x06005696 RID: 22166 RVA: 0x0016D9F0 File Offset: 0x0016BBF0
		public void SameParameters(ParameterInfoCollection otherParameters, out bool sameQueryParameters, out bool sameSnapshotParameters)
		{
			sameQueryParameters = true;
			sameSnapshotParameters = true;
			bool flag = false;
			int num = 0;
			while (num < this.Count && !flag)
			{
				ParameterInfo parameterInfo = this[num];
				ParameterInfo parameterInfo2 = otherParameters[parameterInfo.Name];
				if (!ParameterInfoCollection.SameParameterValues(parameterInfo, parameterInfo2))
				{
					flag = this.UpdateParameterFlagsAndBreak(parameterInfo.UsedInQuery, ref sameQueryParameters, ref sameSnapshotParameters);
				}
				num++;
			}
		}

		// Token: 0x06005697 RID: 22167 RVA: 0x0016DA46 File Offset: 0x0016BC46
		private bool UpdateParameterFlagsAndBreak(bool usedInQuery, ref bool sameQueryParameters, ref bool sameSnapshotParameters)
		{
			if (usedInQuery)
			{
				sameQueryParameters = false;
				if (!sameSnapshotParameters)
				{
					return true;
				}
			}
			else
			{
				sameSnapshotParameters = false;
				if (!sameQueryParameters)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x0016DA60 File Offset: 0x0016BC60
		private static bool SameParameterValues(ParameterInfo thisParameter, ParameterInfo otherParameter)
		{
			Global.Tracer.Assert(thisParameter != null, "thisParameter");
			if (thisParameter == null != (otherParameter == null) || thisParameter.Values == null != (otherParameter.Values == null))
			{
				return false;
			}
			if (thisParameter.Values != null)
			{
				int num = thisParameter.Values.Length;
				if (num != otherParameter.Values.Length)
				{
					return false;
				}
				if (num == 1)
				{
					if (!ParameterBase.ParameterValuesEqual(thisParameter.Values[0], otherParameter.Values[0]))
					{
						return false;
					}
				}
				else
				{
					Hashtable hashtable = new Hashtable();
					for (int i = 0; i < num; i++)
					{
						if (!hashtable.ContainsKey(thisParameter.Values[i].GetHashCode()))
						{
							hashtable.Add(thisParameter.Values[i].GetHashCode(), thisParameter.Values[i]);
						}
					}
					for (int j = 0; j < num; j++)
					{
						int hashCode = otherParameter.Values[j].GetHashCode();
						if (hashtable.ContainsKey(hashCode))
						{
							if (!ParameterBase.ParameterValuesEqual(hashtable[hashCode], otherParameter.Values[j]))
							{
								return false;
							}
							hashtable.Remove(hashCode);
						}
					}
					if (hashtable.Count != 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x0016DB90 File Offset: 0x0016BD90
		private bool SameReportParameters(NameValueCollection otherParams, bool ignoreQueryParams)
		{
			if (otherParams == null || otherParams.Count == 0)
			{
				return true;
			}
			for (int i = 0; i < this.Count; i++)
			{
				ParameterInfo parameterInfo = this[i];
				string[] values = otherParams.GetValues(parameterInfo.Name);
				if ((!ignoreQueryParams || !parameterInfo.UsedInQuery) && values != null)
				{
					if (parameterInfo.Values == null || parameterInfo.Values.Length == 0)
					{
						return false;
					}
					if (values == null || values.Length != parameterInfo.Values.Length)
					{
						return false;
					}
					for (int j = 0; j < parameterInfo.Values.Length; j++)
					{
						object obj = parameterInfo.Values[j];
						object obj2;
						if (!ParameterBase.CastFromString(values[j], out obj2, parameterInfo.DataType, Localization.ClientPrimaryCulture))
						{
							return false;
						}
						if (!ParameterBase.ParameterValuesEqual(obj, obj2))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600569A RID: 22170 RVA: 0x0016DC4D File Offset: 0x0016BE4D
		public bool SameSnapshotParameters(NameValueCollection otherParams)
		{
			return this.SameReportParameters(otherParams, true);
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x0016DC58 File Offset: 0x0016BE58
		public bool SameReportParameters(string passedInParameters)
		{
			if (passedInParameters == null)
			{
				return true;
			}
			ParameterInfoCollection parameterInfoCollection = ParameterInfoCollection.DecodeFromXml(passedInParameters);
			return parameterInfoCollection == null || parameterInfoCollection.Count == 0 || this.SameReportParameters(parameterInfoCollection.AsNameValueCollectionInUserCulture, false);
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x0016DC8C File Offset: 0x0016BE8C
		internal void StoreLabels()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].StoreLabels();
			}
		}

		// Token: 0x17001F97 RID: 8087
		// (get) Token: 0x0600569D RID: 22173 RVA: 0x0016DCB6 File Offset: 0x0016BEB6
		// (set) Token: 0x0600569E RID: 22174 RVA: 0x0016DCBE File Offset: 0x0016BEBE
		public bool Validated
		{
			get
			{
				return this.m_validated;
			}
			set
			{
				this.m_validated = value;
			}
		}

		// Token: 0x17001F98 RID: 8088
		// (get) Token: 0x0600569F RID: 22175 RVA: 0x0016DCC8 File Offset: 0x0016BEC8
		public bool IsAnyParameterDynamic
		{
			get
			{
				foreach (object obj in this)
				{
					ParameterInfo parameterInfo = (ParameterInfo)obj;
					if (parameterInfo.DynamicDefaultValue || parameterInfo.DynamicValidValues || parameterInfo.DynamicPrompt)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17001F99 RID: 8089
		// (get) Token: 0x060056A0 RID: 22176 RVA: 0x0016DD34 File Offset: 0x0016BF34
		// (set) Token: 0x060056A1 RID: 22177 RVA: 0x0016DD3C File Offset: 0x0016BF3C
		public UserProfileState UserProfileState
		{
			get
			{
				return this.m_userProfileState;
			}
			set
			{
				this.m_userProfileState = value;
			}
		}

		// Token: 0x17001F9A RID: 8090
		// (get) Token: 0x060056A2 RID: 22178 RVA: 0x0016DD45 File Offset: 0x0016BF45
		// (set) Token: 0x060056A3 RID: 22179 RVA: 0x0016DD4D File Offset: 0x0016BF4D
		public ParametersGridLayout ParametersLayout
		{
			get
			{
				return this.m_parametersLayout;
			}
			set
			{
				this.m_parametersLayout = value;
			}
		}

		// Token: 0x060056A4 RID: 22180 RVA: 0x0016DD58 File Offset: 0x0016BF58
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (ParameterInfoCollection.m_Declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterInfoCollection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.UserProfileState, Token.Enum),
					new MemberInfo(MemberName.Parameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterInfo),
					new MemberInfo(MemberName.ParametersLayout, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParametersLayout, Lifetime.AddedIn(300))
				});
			}
			return ParameterInfoCollection.m_Declaration;
		}

		// Token: 0x060056A5 RID: 22181 RVA: 0x0016DDC8 File Offset: 0x0016BFC8
		public ParameterInfoCollection GetQueryParameters()
		{
			ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection();
			foreach (object obj in this)
			{
				ParameterInfo parameterInfo = (ParameterInfo)obj;
				if (parameterInfo.UsedInQuery)
				{
					parameterInfoCollection.Add(parameterInfo);
				}
			}
			return parameterInfoCollection;
		}

		// Token: 0x060056A6 RID: 22182 RVA: 0x0016DE2C File Offset: 0x0016C02C
		internal string[] GetParameterNames()
		{
			string[] array = new string[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				array[i] = this[i].Name;
			}
			return array;
		}

		// Token: 0x060056A7 RID: 22183 RVA: 0x0016DE68 File Offset: 0x0016C068
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].IndexInCollection = i;
			}
			writer.RegisterDeclaration(ParameterInfoCollection.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Parameters)
				{
					if (memberName != MemberName.UserProfileState)
					{
						if (memberName != MemberName.ParametersLayout)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_parametersLayout);
						}
					}
					else
					{
						writer.WriteEnum((int)this.m_userProfileState);
					}
				}
				else
				{
					writer.Write(this);
				}
			}
		}

		// Token: 0x060056A8 RID: 22184 RVA: 0x0016DF04 File Offset: 0x0016C104
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ParameterInfoCollection.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Parameters)
				{
					if (memberName != MemberName.UserProfileState)
					{
						if (memberName != MemberName.ParametersLayout)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_parametersLayout = (ParametersGridLayout)reader.ReadRIFObject();
						}
					}
					else
					{
						this.m_userProfileState = (UserProfileState)reader.ReadEnum();
					}
				}
				else
				{
					reader.ReadListOfRIFObjects(this);
				}
			}
			for (int i = 0; i < this.Count; i++)
			{
				this[i].ResolveDependencies(this);
			}
		}

		// Token: 0x060056A9 RID: 22185 RVA: 0x0016DFA2 File Offset: 0x0016C1A2
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x060056AA RID: 22186 RVA: 0x0016DFAF File Offset: 0x0016C1AF
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterInfoCollection;
		}

		// Token: 0x04002DC7 RID: 11719
		private ParametersGridLayout m_parametersLayout;

		// Token: 0x04002DC8 RID: 11720
		private UserProfileState m_userProfileState;

		// Token: 0x04002DC9 RID: 11721
		private bool m_validated;

		// Token: 0x04002DCA RID: 11722
		private const string SerializationXmlPropName = "Xml";

		// Token: 0x04002DCB RID: 11723
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ParameterInfoCollection.GetDeclaration();
	}
}
