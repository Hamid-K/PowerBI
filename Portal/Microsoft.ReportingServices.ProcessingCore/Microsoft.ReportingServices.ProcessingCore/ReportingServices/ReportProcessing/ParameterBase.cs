using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000618 RID: 1560
	[Serializable]
	public abstract class ParameterBase : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06005585 RID: 21893 RVA: 0x00168956 File Offset: 0x00166B56
		public ParameterBase()
		{
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x0016896D File Offset: 0x00166B6D
		internal ParameterBase(ParameterValue source)
		{
			this.m_dataType = DataType.Object;
			this.m_name = source.Name;
			this.m_usedInQuery = false;
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x001689A0 File Offset: 0x00166BA0
		internal ParameterBase(DataSetParameterValue source, bool usedInQuery)
		{
			this.m_dataType = DataType.Object;
			this.m_name = source.UniqueName;
			this.m_nullable = source.Nullable;
			this.m_multiValue = source.MultiValue;
			this.m_allowBlank = false;
			this.m_promptUser = !source.ReadOnly;
			this.m_usedInQuery = usedInQuery;
			this.m_useAllValidValues = source.UseAllValidValues;
			if (source.Value != null && !source.Value.IsExpression)
			{
				this.m_defaultValues = new object[1];
				this.m_defaultValues[0] = source.Value.Value;
			}
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x00168A4C File Offset: 0x00166C4C
		internal ParameterBase(ParameterBase source)
		{
			this.m_name = source.m_name;
			this.m_dataType = source.m_dataType;
			this.m_nullable = source.m_nullable;
			this.m_promptUser = source.m_promptUser;
			this.m_allowBlank = source.m_allowBlank;
			this.m_multiValue = source.m_multiValue;
			this.m_useAllValidValues = source.m_useAllValidValues;
			if (source.m_defaultValues != null)
			{
				int num = source.m_defaultValues.Length;
				this.m_defaultValues = new object[num];
				for (int i = 0; i < num; i++)
				{
					this.m_defaultValues[i] = source.m_defaultValues[i];
				}
			}
			this.m_usedInQuery = source.m_usedInQuery;
		}

		// Token: 0x17001F48 RID: 8008
		// (get) Token: 0x06005589 RID: 21897 RVA: 0x00168B07 File Offset: 0x00166D07
		internal Microsoft.ReportingServices.ReportProcessing.ObjectType ParameterObjectType
		{
			get
			{
				if (this.m_dataType == DataType.Object)
				{
					return Microsoft.ReportingServices.ReportProcessing.ObjectType.QueryParameter;
				}
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter;
			}
		}

		// Token: 0x0600558A RID: 21898 RVA: 0x00168B18 File Offset: 0x00166D18
		internal static Microsoft.ReportingServices.ReportProcessing.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportProcessing.Persistence.Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.None, new MemberInfoList
			{
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.Name, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.String),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.DataType, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Enum),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.Nullable, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.Prompt, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.String),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.UsedInQuery, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.AllowBlank, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.MultiValue, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.DefaultValue, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Array, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.Variant),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.PromptUser, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Boolean)
			});
		}

		// Token: 0x0600558B RID: 21899 RVA: 0x00168BF0 File Offset: 0x00166DF0
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetNewDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo>
			{
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Name, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.String),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DataType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Enum),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Nullable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.UsedInQuery, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.AllowBlank, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.MultiValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DefaultValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Object),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.PromptUser, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.UseAllValidValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Boolean)
			});
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x00168CC0 File Offset: 0x00166EC0
		internal void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ParameterBase.m_Declaration);
			while (writer.NextMember())
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Nullable)
				{
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Name)
					{
						writer.Write(this.m_name);
						continue;
					}
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DataType)
					{
						writer.WriteEnum((int)this.m_dataType);
						continue;
					}
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Nullable)
					{
						writer.Write(this.m_nullable);
						continue;
					}
				}
				else
				{
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.PromptUser)
					{
						writer.Write(this.m_promptUser);
						continue;
					}
					switch (memberName)
					{
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.UsedInQuery:
						writer.Write(this.m_usedInQuery);
						continue;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.UsedOnlyInParameters:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ValidValues:
						break;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.AllowBlank:
						writer.Write(this.m_allowBlank);
						continue;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.MultiValue:
						writer.Write(this.m_multiValue);
						continue;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DefaultValue:
						writer.Write(this.m_defaultValues);
						continue;
					default:
						if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.UseAllValidValues)
						{
							writer.Write(this.m_useAllValidValues);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x00168DEC File Offset: 0x00166FEC
		internal void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ParameterBase.m_Declaration);
			while (reader.NextMember())
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Nullable)
				{
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Name)
					{
						this.m_name = reader.ReadString();
						continue;
					}
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DataType)
					{
						this.m_dataType = (DataType)reader.ReadEnum();
						continue;
					}
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Nullable)
					{
						this.m_nullable = reader.ReadBoolean();
						continue;
					}
				}
				else
				{
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.PromptUser)
					{
						this.m_promptUser = reader.ReadBoolean();
						continue;
					}
					switch (memberName)
					{
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.UsedInQuery:
						this.m_usedInQuery = reader.ReadBoolean();
						continue;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.UsedOnlyInParameters:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ValidValues:
						break;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.AllowBlank:
						this.m_allowBlank = reader.ReadBoolean();
						continue;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.MultiValue:
						this.m_multiValue = reader.ReadBoolean();
						continue;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DefaultValue:
						this.m_defaultValues = reader.ReadVariantArray();
						continue;
					default:
						if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.UseAllValidValues)
						{
							this.m_useAllValidValues = reader.ReadBoolean();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x00168F16 File Offset: 0x00167116
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x00168F1F File Offset: 0x0016711F
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x00168F28 File Offset: 0x00167128
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x00168F35 File Offset: 0x00167135
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterBase;
		}

		// Token: 0x17001F49 RID: 8009
		// (get) Token: 0x06005592 RID: 21906 RVA: 0x00168F3C File Offset: 0x0016713C
		// (set) Token: 0x06005593 RID: 21907 RVA: 0x00168F44 File Offset: 0x00167144
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17001F4A RID: 8010
		// (get) Token: 0x06005594 RID: 21908 RVA: 0x00168F4D File Offset: 0x0016714D
		// (set) Token: 0x06005595 RID: 21909 RVA: 0x00168F55 File Offset: 0x00167155
		public DataType DataType
		{
			get
			{
				return this.m_dataType;
			}
			set
			{
				this.m_dataType = value;
			}
		}

		// Token: 0x17001F4B RID: 8011
		// (get) Token: 0x06005596 RID: 21910 RVA: 0x00168F5E File Offset: 0x0016715E
		// (set) Token: 0x06005597 RID: 21911 RVA: 0x00168F66 File Offset: 0x00167166
		public bool Nullable
		{
			get
			{
				return this.m_nullable;
			}
			set
			{
				this.m_nullable = value;
			}
		}

		// Token: 0x17001F4C RID: 8012
		// (get) Token: 0x06005598 RID: 21912
		// (set) Token: 0x06005599 RID: 21913
		public abstract string Prompt { get; set; }

		// Token: 0x17001F4D RID: 8013
		// (get) Token: 0x0600559A RID: 21914 RVA: 0x00168F6F File Offset: 0x0016716F
		// (set) Token: 0x0600559B RID: 21915 RVA: 0x00168F77 File Offset: 0x00167177
		public bool PromptUser
		{
			get
			{
				return this.m_promptUser;
			}
			set
			{
				this.m_promptUser = value;
			}
		}

		// Token: 0x17001F4E RID: 8014
		// (get) Token: 0x0600559C RID: 21916 RVA: 0x00168F80 File Offset: 0x00167180
		// (set) Token: 0x0600559D RID: 21917 RVA: 0x00168F88 File Offset: 0x00167188
		public bool AllowBlank
		{
			get
			{
				return this.m_allowBlank;
			}
			set
			{
				this.m_allowBlank = value;
			}
		}

		// Token: 0x17001F4F RID: 8015
		// (get) Token: 0x0600559E RID: 21918 RVA: 0x00168F91 File Offset: 0x00167191
		// (set) Token: 0x0600559F RID: 21919 RVA: 0x00168F99 File Offset: 0x00167199
		public bool MultiValue
		{
			get
			{
				return this.m_multiValue;
			}
			set
			{
				this.m_multiValue = value;
			}
		}

		// Token: 0x17001F50 RID: 8016
		// (get) Token: 0x060055A0 RID: 21920 RVA: 0x00168FA2 File Offset: 0x001671A2
		// (set) Token: 0x060055A1 RID: 21921 RVA: 0x00168FAA File Offset: 0x001671AA
		public object[] DefaultValues
		{
			get
			{
				return this.m_defaultValues;
			}
			set
			{
				this.m_defaultValues = value;
			}
		}

		// Token: 0x17001F51 RID: 8017
		// (get) Token: 0x060055A2 RID: 21922 RVA: 0x00168FB3 File Offset: 0x001671B3
		// (set) Token: 0x060055A3 RID: 21923 RVA: 0x00168FBB File Offset: 0x001671BB
		internal Hashtable Dependencies
		{
			get
			{
				return this.m_dependencies;
			}
			set
			{
				this.m_dependencies = value;
			}
		}

		// Token: 0x17001F52 RID: 8018
		// (get) Token: 0x060055A4 RID: 21924 RVA: 0x00168FC4 File Offset: 0x001671C4
		// (set) Token: 0x060055A5 RID: 21925 RVA: 0x00168FCC File Offset: 0x001671CC
		public bool UsedInQuery
		{
			get
			{
				return this.m_usedInQuery;
			}
			set
			{
				this.m_usedInQuery = value;
			}
		}

		// Token: 0x17001F53 RID: 8019
		// (get) Token: 0x060055A6 RID: 21926 RVA: 0x00168FD5 File Offset: 0x001671D5
		internal ParameterBase.UsedInQueryType UsedInQueryAsDefined
		{
			get
			{
				return this.m_usedInQueryAsDefined;
			}
		}

		// Token: 0x17001F54 RID: 8020
		// (get) Token: 0x060055A7 RID: 21927 RVA: 0x00168FDD File Offset: 0x001671DD
		// (set) Token: 0x060055A8 RID: 21928 RVA: 0x00168FE5 File Offset: 0x001671E5
		public bool UseAllValidValues
		{
			get
			{
				return this.m_useAllValidValues;
			}
			set
			{
				this.m_useAllValidValues = value;
			}
		}

		// Token: 0x060055A9 RID: 21929 RVA: 0x00168FF0 File Offset: 0x001671F0
		internal static bool ValidateValueForNull(object newValue, bool nullable, ErrorContext errorContext, Microsoft.ReportingServices.ReportProcessing.ObjectType parameterType, string parameterName, string parameterValueProperty)
		{
			bool flag = true;
			bool flag2 = errorContext is PublishingErrorContext;
			if (newValue == null && !nullable)
			{
				flag = false;
				if (errorContext != null)
				{
					errorContext.Register(flag2 ? ProcessingErrorCode.rsParameterValueNullOrBlank : ProcessingErrorCode.rsParameterValueDefinitionMismatch, flag2 ? Severity.Error : Severity.Warning, parameterType, parameterName, "Nullable", new string[] { parameterValueProperty });
				}
			}
			return flag;
		}

		// Token: 0x060055AA RID: 21930 RVA: 0x00169048 File Offset: 0x00167248
		internal bool ValidateValueForBlank(object newValue, ErrorContext errorContext, string parameterValueProperty)
		{
			bool flag = true;
			bool flag2 = errorContext is PublishingErrorContext;
			if (this.DataType == DataType.String && !this.AllowBlank && (string)newValue == string.Empty)
			{
				flag = false;
				if (errorContext != null)
				{
					errorContext.Register(flag2 ? ProcessingErrorCode.rsParameterValueNullOrBlank : ProcessingErrorCode.rsParameterValueDefinitionMismatch, flag2 ? Severity.Error : Severity.Warning, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, this.m_name, "AllowBlank", new string[] { parameterValueProperty });
				}
			}
			return flag;
		}

		// Token: 0x060055AB RID: 21931 RVA: 0x001690BF File Offset: 0x001672BF
		internal void ValidateValue(object newValue, ErrorContext errorContext, Microsoft.ReportingServices.ReportProcessing.ObjectType parameterType, string parameterValueProperty)
		{
			ParameterBase.ValidateValueForNull(newValue, this.Nullable, errorContext, parameterType, this.Name, parameterValueProperty);
			this.ValidateValueForBlank(newValue, errorContext, parameterValueProperty);
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x001690E4 File Offset: 0x001672E4
		public virtual void Parse(string name, List<string> defaultValues, string type, string nullable, object prompt, string promptUser, string allowBlank, string multiValue, string usedInQuery, bool hidden, ErrorContext errorContext, CultureInfo language, string useAllValidValues)
		{
			if (name == null || name.Length == 0)
			{
				throw new MissingElementException("Name");
			}
			this.m_name = name;
			if (type == null || type.Length == 0)
			{
				this.m_dataType = DataType.String;
			}
			else
			{
				try
				{
					this.m_dataType = (DataType)Enum.Parse(typeof(DataType), type, true);
				}
				catch (ArgumentException)
				{
					if (errorContext == null)
					{
						throw new ElementTypeMismatchException("Type");
					}
					errorContext.Register(ProcessingErrorCode.rsParameterPropertyTypeMismatch, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.Parameter, name, "DataType", Array.Empty<string>());
				}
			}
			if (nullable == null || nullable.Length == 0)
			{
				this.m_nullable = false;
			}
			else
			{
				try
				{
					this.m_nullable = bool.Parse(nullable);
				}
				catch (FormatException)
				{
					if (errorContext == null)
					{
						throw new ElementTypeMismatchException("Nullable");
					}
					errorContext.Register(ProcessingErrorCode.rsParameterPropertyTypeMismatch, Severity.Error, this.ParameterObjectType, name, "Nullable", Array.Empty<string>());
				}
			}
			if (allowBlank == null || allowBlank.Length == 0)
			{
				this.m_allowBlank = false;
			}
			else
			{
				try
				{
					this.m_allowBlank = bool.Parse(allowBlank);
				}
				catch (FormatException)
				{
					if (errorContext == null)
					{
						throw new ElementTypeMismatchException("AllowBlank");
					}
					errorContext.Register(ProcessingErrorCode.rsParameterPropertyTypeMismatch, Severity.Error, this.ParameterObjectType, name, "AllowBlank", Array.Empty<string>());
				}
			}
			if (multiValue == null || multiValue.Length == 0 || this.m_dataType == DataType.Boolean)
			{
				this.m_multiValue = false;
			}
			else
			{
				try
				{
					this.m_multiValue = bool.Parse(multiValue);
				}
				catch (FormatException)
				{
					if (errorContext == null)
					{
						throw new ElementTypeMismatchException("MultiValue");
					}
					errorContext.Register(ProcessingErrorCode.rsParameterPropertyTypeMismatch, Severity.Error, this.ParameterObjectType, name, "MultiValue", Array.Empty<string>());
				}
			}
			if (promptUser == null || promptUser == string.Empty)
			{
				if (prompt == null)
				{
					this.m_promptUser = false;
				}
				else
				{
					this.m_promptUser = true;
				}
			}
			else
			{
				try
				{
					this.m_promptUser = bool.Parse(promptUser);
				}
				catch (FormatException)
				{
					throw new ElementTypeMismatchException("PromptUser");
				}
			}
			if (defaultValues == null)
			{
				this.m_defaultValues = null;
			}
			else
			{
				int count = defaultValues.Count;
				this.m_defaultValues = new object[count];
				for (int i = 0; i < count; i++)
				{
					object obj;
					if (!ParameterBase.CastFromString(defaultValues[i], out obj, this.m_dataType, language))
					{
						if (errorContext == null)
						{
							throw new ReportParameterTypeMismatchException(name);
						}
						errorContext.Register(ProcessingErrorCode.rsParameterPropertyTypeMismatch, Severity.Error, this.ParameterObjectType, name, "DefaultValue", Array.Empty<string>());
					}
					else
					{
						this.ValidateValue(obj, errorContext, this.ParameterObjectType, "DefaultValue");
					}
					this.m_defaultValues[i] = obj;
				}
			}
			this.m_usedInQuery = true;
			if (usedInQuery == null || usedInQuery.Length == 0)
			{
				this.m_usedInQueryAsDefined = ParameterBase.UsedInQueryType.Auto;
			}
			else
			{
				try
				{
					this.m_usedInQueryAsDefined = (ParameterBase.UsedInQueryType)Enum.Parse(typeof(ParameterBase.UsedInQueryType), usedInQuery, true);
				}
				catch (ArgumentException)
				{
					if (errorContext == null)
					{
						throw new ElementTypeMismatchException("UsedInQuery");
					}
					errorContext.Register(ProcessingErrorCode.rsParameterPropertyTypeMismatch, Severity.Error, this.ParameterObjectType, name, "MultiValue", Array.Empty<string>());
				}
				if (this.m_usedInQueryAsDefined == ParameterBase.UsedInQueryType.False)
				{
					this.m_usedInQuery = false;
				}
				else if (this.m_usedInQueryAsDefined == ParameterBase.UsedInQueryType.True)
				{
					this.m_usedInQuery = true;
				}
			}
			if (usedInQuery == null || usedInQuery.Length == 0)
			{
				this.m_usedInQueryAsDefined = ParameterBase.UsedInQueryType.Auto;
			}
			else
			{
				try
				{
					this.m_usedInQueryAsDefined = (ParameterBase.UsedInQueryType)Enum.Parse(typeof(ParameterBase.UsedInQueryType), usedInQuery, true);
				}
				catch (ArgumentException)
				{
					throw new ElementTypeMismatchException("UsedInQuery");
				}
				if (this.m_usedInQueryAsDefined == ParameterBase.UsedInQueryType.False)
				{
					this.m_usedInQuery = false;
				}
				else if (this.m_usedInQueryAsDefined == ParameterBase.UsedInQueryType.True)
				{
					this.m_usedInQuery = true;
				}
			}
			if (useAllValidValues == null || useAllValidValues.Length == 0 || this.m_dataType == DataType.Boolean)
			{
				this.m_useAllValidValues = false;
				return;
			}
			try
			{
				this.m_useAllValidValues = bool.Parse(useAllValidValues);
			}
			catch (FormatException)
			{
				if (errorContext == null)
				{
					throw new ElementTypeMismatchException("UseAllValidValues");
				}
				errorContext.Register(ProcessingErrorCode.rsParameterPropertyTypeMismatch, Severity.Error, this.ParameterObjectType, name, "UseAllValidValues", Array.Empty<string>());
			}
		}

		// Token: 0x060055AD RID: 21933 RVA: 0x00169514 File Offset: 0x00167714
		internal static bool Cast(object oldValue, DataType oldType, out object newValue, DataType newType, CultureInfo language)
		{
			if (oldValue == null)
			{
				newValue = null;
				return true;
			}
			if (oldType <= DataType.Integer)
			{
				if (oldType == DataType.Object)
				{
					newValue = oldValue;
					return true;
				}
				if (oldType == DataType.Boolean)
				{
					return ParameterBase.CastFromBoolean((bool)oldValue, out newValue, newType, language);
				}
				if (oldType == DataType.Integer)
				{
					return ParameterBase.CastFromInteger((int)oldValue, out newValue, newType, language);
				}
			}
			else
			{
				if (oldType == DataType.Float)
				{
					return ParameterBase.CastFromDouble((double)oldValue, out newValue, newType, language);
				}
				if (oldType != DataType.DateTime)
				{
					if (oldType == DataType.String)
					{
						return ParameterBase.CastFromString((string)oldValue, out newValue, newType, language);
					}
				}
				else
				{
					if (oldValue is DateTimeOffset)
					{
						return ParameterBase.CastFromDateTimeOffset((DateTimeOffset)oldValue, out newValue, newType, language);
					}
					return ParameterBase.CastFromDateTime((DateTime)oldValue, out newValue, newType, language);
				}
			}
			throw new InternalCatalogException("Parameter type is not one of the supported types in Cast");
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x001695C8 File Offset: 0x001677C8
		internal static bool DecodeObjectFromBase64String(string originalValue, out object newValue)
		{
			newValue = null;
			if (string.IsNullOrEmpty(originalValue))
			{
				return true;
			}
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(originalValue)))
				{
					ProcessingRIFObjectCreator processingRIFObjectCreator = new ProcessingRIFObjectCreator(null, null);
					Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader intermediateFormatReader = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader(memoryStream, processingRIFObjectCreator);
					RIFVariantContainer rifvariantContainer = (RIFVariantContainer)intermediateFormatReader.ReadRIFObject();
					newValue = rifvariantContainer.Value;
				}
			}
			catch (Exception ex)
			{
				throw new InternalCatalogException(ex, "Parameter value decoding failed for base64 encoded string='" + originalValue + "'");
			}
			return true;
		}

		// Token: 0x060055AF RID: 21935 RVA: 0x00169658 File Offset: 0x00167858
		public static bool CastFromString(string oldString, out object newValue, DataType newType, CultureInfo language)
		{
			newValue = null;
			if (oldString == null)
			{
				return true;
			}
			if (newType <= DataType.Integer)
			{
				if (newType == DataType.Object)
				{
					return ParameterBase.DecodeObjectFromBase64String(oldString, out newValue);
				}
				if (newType != DataType.Boolean)
				{
					if (newType != DataType.Integer)
					{
						goto IL_0196;
					}
				}
				else
				{
					if (string.Compare(oldString, "true", true, language) == 0 || string.Compare(oldString, "enable", true, language) == 0 || string.Compare(oldString, "enabled", true, language) == 0 || string.Compare(oldString, "yes", true, language) == 0 || string.Compare(oldString, "on", true, language) == 0 || string.Compare(oldString, "+", true, language) == 0)
					{
						newValue = true;
						return true;
					}
					if (string.Compare(oldString, "false", true, language) == 0 || string.Compare(oldString, "disable", true, language) == 0 || string.Compare(oldString, "disabled", true, language) == 0 || string.Compare(oldString, "no", true, language) == 0 || string.Compare(oldString, "off", true, language) == 0 || string.Compare(oldString, "-", true, language) == 0)
					{
						newValue = false;
						return true;
					}
					return false;
				}
			}
			else if (newType != DataType.Float)
			{
				if (newType == DataType.DateTime)
				{
					goto IL_016A;
				}
				if (newType != DataType.String)
				{
					goto IL_0196;
				}
				newValue = oldString;
				return true;
			}
			else
			{
				try
				{
					newValue = double.Parse(oldString, language);
					return true;
				}
				catch (Exception ex)
				{
					if (ex is FormatException || ex is OverflowException)
					{
						return false;
					}
					throw;
				}
			}
			try
			{
				newValue = int.Parse(oldString, language);
				return true;
			}
			catch (Exception ex2)
			{
				if (ex2 is FormatException || ex2 is OverflowException)
				{
					return false;
				}
				throw;
			}
			IL_016A:
			DateTimeOffset dateTimeOffset;
			bool flag;
			if (Microsoft.ReportingServices.Common.DateTimeUtil.TryParseDateTime(oldString, language, out dateTimeOffset, out flag))
			{
				if (flag)
				{
					newValue = dateTimeOffset;
				}
				else
				{
					newValue = dateTimeOffset.DateTime;
				}
				return true;
			}
			return false;
			IL_0196:
			throw new InternalCatalogException("Parameter type is not one of the supported types in Cast");
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x00169824 File Offset: 0x00167A24
		internal static bool CastFromBoolean(bool oldBoolean, out object newValue, DataType newType, CultureInfo language)
		{
			newValue = null;
			if (newType <= DataType.Integer)
			{
				if (newType == DataType.Object)
				{
					newValue = oldBoolean;
					return true;
				}
				if (newType == DataType.Boolean)
				{
					newValue = oldBoolean;
					return true;
				}
				if (newType == DataType.Integer)
				{
					newValue = ((oldBoolean > false) ? 1 : 0);
					return true;
				}
			}
			else
			{
				if (newType == DataType.Float)
				{
					newValue = oldBoolean > false;
					return true;
				}
				if (newType == DataType.DateTime)
				{
					return false;
				}
				if (newType == DataType.String)
				{
					newValue = oldBoolean.ToString(language);
					return true;
				}
			}
			throw new InternalCatalogException("Parameter type is not one of the supported types in Cast");
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x001698A0 File Offset: 0x00167AA0
		internal static bool CastFromDouble(double oldDouble, out object newValue, DataType newType, CultureInfo language)
		{
			newValue = null;
			checked
			{
				if (newType <= DataType.Integer)
				{
					if (newType == DataType.Object)
					{
						newValue = oldDouble;
						return true;
					}
					if (newType == DataType.Boolean)
					{
						newValue = oldDouble != 0.0;
						return true;
					}
					if (newType == DataType.Integer)
					{
						try
						{
							newValue = (int)oldDouble;
						}
						catch (OverflowException)
						{
							return false;
						}
						return true;
					}
				}
				else
				{
					if (newType == DataType.Float)
					{
						newValue = oldDouble;
						return true;
					}
					if (newType == DataType.DateTime)
					{
						try
						{
							newValue = new DateTime((long)oldDouble);
						}
						catch (Exception ex)
						{
							if (ex is ArgumentOutOfRangeException || ex is OverflowException)
							{
								return false;
							}
							throw;
						}
						return true;
					}
					if (newType == DataType.String)
					{
						newValue = oldDouble.ToString(language);
						return true;
					}
				}
				throw new InternalCatalogException("Parameter type is not one of the supported types in Cast");
			}
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x00169978 File Offset: 0x00167B78
		internal static bool CastFromInteger(int oldInteger, out object newValue, DataType newType, CultureInfo language)
		{
			newValue = null;
			if (newType <= DataType.Integer)
			{
				if (newType == DataType.Object)
				{
					newValue = oldInteger;
					return true;
				}
				if (newType == DataType.Boolean)
				{
					newValue = oldInteger != 0;
					return true;
				}
				if (newType == DataType.Integer)
				{
					newValue = oldInteger;
					return true;
				}
			}
			else
			{
				if (newType == DataType.Float)
				{
					newValue = (double)oldInteger;
					return true;
				}
				if (newType == DataType.DateTime)
				{
					try
					{
						newValue = new DateTime((long)oldInteger);
					}
					catch (Exception ex)
					{
						if (ex is ArgumentOutOfRangeException || ex is OverflowException)
						{
							return false;
						}
						throw;
					}
					return true;
				}
				if (newType == DataType.String)
				{
					newValue = oldInteger.ToString(language);
					return true;
				}
			}
			throw new InternalCatalogException("Parameter type is not one of the supported types in Cast");
		}

		// Token: 0x060055B3 RID: 21939 RVA: 0x00169A2C File Offset: 0x00167C2C
		internal static bool CastFromDateTime(DateTime oldDateTime, out object newValue, DataType newType, CultureInfo language)
		{
			newValue = null;
			if (newType <= DataType.Integer)
			{
				if (newType == DataType.Object)
				{
					newValue = oldDateTime;
					return true;
				}
				if (newType == DataType.Boolean)
				{
					return false;
				}
				if (newType != DataType.Integer)
				{
					goto IL_0077;
				}
				try
				{
					newValue = Convert.ToInt32(oldDateTime.Ticks);
					return true;
				}
				catch (OverflowException)
				{
					return false;
				}
			}
			else
			{
				if (newType == DataType.Float)
				{
					newValue = oldDateTime.Ticks;
					return true;
				}
				if (newType != DataType.DateTime)
				{
					if (newType != DataType.String)
					{
						goto IL_0077;
					}
					newValue = oldDateTime.ToString(language);
					return true;
				}
			}
			newValue = oldDateTime;
			return true;
			IL_0077:
			throw new InternalCatalogException("Parameter type is not one of the supported types in Cast");
		}

		// Token: 0x060055B4 RID: 21940 RVA: 0x00169ACC File Offset: 0x00167CCC
		internal static bool CastFromDateTimeOffset(DateTimeOffset oldDateTime, out object newValue, DataType newType, CultureInfo language)
		{
			newValue = null;
			if (newType <= DataType.Integer)
			{
				if (newType == DataType.Object)
				{
					newValue = oldDateTime;
					return true;
				}
				if (newType == DataType.Boolean)
				{
					return false;
				}
				if (newType == DataType.Integer)
				{
					return false;
				}
			}
			else
			{
				if (newType == DataType.Float)
				{
					return false;
				}
				if (newType == DataType.DateTime)
				{
					newValue = oldDateTime;
					return true;
				}
				if (newType == DataType.String)
				{
					newValue = oldDateTime.ToString(language);
					return true;
				}
			}
			throw new InternalCatalogException("Parameter type is not one of the supported types in Cast");
		}

		// Token: 0x060055B5 RID: 21941 RVA: 0x00169B31 File Offset: 0x00167D31
		public static bool ParameterValuesEqual(object o1, object o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x00169B3A File Offset: 0x00167D3A
		internal static bool IsSharedDataSetParameterObjectType(Microsoft.ReportingServices.ReportProcessing.ObjectType ot)
		{
			if (ot != Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter)
			{
				if (ot == Microsoft.ReportingServices.ReportProcessing.ObjectType.QueryParameter)
				{
					return true;
				}
				if (ot != Microsoft.ReportingServices.ReportProcessing.ObjectType.Parameter)
				{
					Global.Tracer.Assert(false, "Unknown ObjectType: {0}", new object[] { ot });
					return Microsoft.ReportingServices.ReportProcessing.ObjectType.QueryParameter == ot;
				}
			}
			return false;
		}

		// Token: 0x04002D69 RID: 11625
		internal const string NameXmlElement = "Name";

		// Token: 0x04002D6A RID: 11626
		internal const string TypeXmlElement = "Type";

		// Token: 0x04002D6B RID: 11627
		internal const string NullableXmlElement = "Nullable";

		// Token: 0x04002D6C RID: 11628
		internal const string AllowBlankXmlElement = "AllowBlank";

		// Token: 0x04002D6D RID: 11629
		internal const string MultiValueXmlElement = "MultiValue";

		// Token: 0x04002D6E RID: 11630
		internal const string PromptXmlElement = "Prompt";

		// Token: 0x04002D6F RID: 11631
		internal const string PromptUserXmlElement = "PromptUser";

		// Token: 0x04002D70 RID: 11632
		internal const string ValueXmlElement = "Value";

		// Token: 0x04002D71 RID: 11633
		internal const string UsedInQueryXmlElement = "UsedInQuery";

		// Token: 0x04002D72 RID: 11634
		internal const string DefaultValuesXmlElement = "DefaultValues";

		// Token: 0x04002D73 RID: 11635
		internal const string ValidValuesXmlElement = "ValidValues";

		// Token: 0x04002D74 RID: 11636
		internal const string StateXmlElement = "State";

		// Token: 0x04002D75 RID: 11637
		internal const string UseAllValidValuesXmlElement = "UseAllValidValues";

		// Token: 0x04002D76 RID: 11638
		private string m_name;

		// Token: 0x04002D77 RID: 11639
		private DataType m_dataType = DataType.String;

		// Token: 0x04002D78 RID: 11640
		private bool m_nullable;

		// Token: 0x04002D79 RID: 11641
		private bool m_promptUser;

		// Token: 0x04002D7A RID: 11642
		private bool m_usedInQuery;

		// Token: 0x04002D7B RID: 11643
		private bool m_allowBlank;

		// Token: 0x04002D7C RID: 11644
		private bool m_multiValue;

		// Token: 0x04002D7D RID: 11645
		private object[] m_defaultValues;

		// Token: 0x04002D7E RID: 11646
		private bool m_useAllValidValues;

		// Token: 0x04002D7F RID: 11647
		[NonSerialized]
		private ParameterBase.UsedInQueryType m_usedInQueryAsDefined = ParameterBase.UsedInQueryType.Auto;

		// Token: 0x04002D80 RID: 11648
		[NonSerialized]
		private Hashtable m_dependencies;

		// Token: 0x04002D81 RID: 11649
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ParameterBase.GetNewDeclaration();

		// Token: 0x02000C8F RID: 3215
		internal enum UsedInQueryType
		{
			// Token: 0x04004A0A RID: 18954
			False,
			// Token: 0x04004A0B RID: 18955
			True,
			// Token: 0x04004A0C RID: 18956
			Auto
		}
	}
}
