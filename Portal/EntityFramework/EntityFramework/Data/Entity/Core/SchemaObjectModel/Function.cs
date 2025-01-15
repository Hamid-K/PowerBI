using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002F3 RID: 755
	internal class Function : SchemaType
	{
		// Token: 0x060023E9 RID: 9193 RVA: 0x00065888 File Offset: 0x00063A88
		internal static void RemoveTypeModifier(ref string type, out TypeModifier typeModifier, out bool isRefType)
		{
			isRefType = false;
			typeModifier = TypeModifier.None;
			Match match = Function._typeParser.Match(type);
			if (match.Success)
			{
				type = match.Groups["typeName"].Value;
				string value = match.Groups["modifier"].Value;
				if (value != null)
				{
					if (value == "Collection")
					{
						typeModifier = TypeModifier.Array;
						return;
					}
					if (!(value == "Ref"))
					{
						return;
					}
					isRefType = true;
					return;
				}
			}
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x00065904 File Offset: 0x00063B04
		internal static string GetTypeNameForErrorMessage(SchemaType type, CollectionKind colKind, bool isRef)
		{
			string text = type.FQName;
			if (isRef)
			{
				text = "Ref(" + text + ")";
			}
			if (colKind == CollectionKind.Bag)
			{
				text = "Collection(" + text + ")";
			}
			return text;
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x00065942 File Offset: 0x00063B42
		public Function(Schema parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x00065952 File Offset: 0x00063B52
		// (set) Token: 0x060023ED RID: 9197 RVA: 0x0006595A File Offset: 0x00063B5A
		public bool IsAggregate
		{
			get
			{
				return this._isAggregate;
			}
			internal set
			{
				this._isAggregate = value;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x00065963 File Offset: 0x00063B63
		// (set) Token: 0x060023EF RID: 9199 RVA: 0x0006596B File Offset: 0x00063B6B
		public bool IsBuiltIn
		{
			get
			{
				return this._isBuiltIn;
			}
			internal set
			{
				this._isBuiltIn = value;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x00065974 File Offset: 0x00063B74
		// (set) Token: 0x060023F1 RID: 9201 RVA: 0x0006597C File Offset: 0x00063B7C
		public bool IsNiladicFunction
		{
			get
			{
				return this._isNiladicFunction;
			}
			internal set
			{
				this._isNiladicFunction = value;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x00065985 File Offset: 0x00063B85
		// (set) Token: 0x060023F3 RID: 9203 RVA: 0x0006598D File Offset: 0x00063B8D
		public bool IsComposable
		{
			get
			{
				return this._isComposable;
			}
			internal set
			{
				this._isComposable = value;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x00065996 File Offset: 0x00063B96
		public string CommandText
		{
			get
			{
				if (this._commandText != null)
				{
					return this._commandText.CommandText;
				}
				return null;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060023F5 RID: 9205 RVA: 0x000659AD File Offset: 0x00063BAD
		// (set) Token: 0x060023F6 RID: 9206 RVA: 0x000659B5 File Offset: 0x00063BB5
		public ParameterTypeSemantics ParameterTypeSemantics
		{
			get
			{
				return this._parameterTypeSemantics;
			}
			internal set
			{
				this._parameterTypeSemantics = value;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x000659BE File Offset: 0x00063BBE
		// (set) Token: 0x060023F8 RID: 9208 RVA: 0x000659C6 File Offset: 0x00063BC6
		public string StoreFunctionName
		{
			get
			{
				return this._storeFunctionName;
			}
			internal set
			{
				this._storeFunctionName = value;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x000659CF File Offset: 0x00063BCF
		public virtual SchemaType Type
		{
			get
			{
				if (this._returnTypeList != null)
				{
					return this._returnTypeList[0].Type;
				}
				return this._type;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x000659F1 File Offset: 0x00063BF1
		public IList<ReturnType> ReturnTypeList
		{
			get
			{
				if (this._returnTypeList == null)
				{
					return null;
				}
				return new ReadOnlyCollection<ReturnType>(this._returnTypeList);
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x00065A08 File Offset: 0x00063C08
		public SchemaElementLookUpTable<Parameter> Parameters
		{
			get
			{
				if (this._parameters == null)
				{
					this._parameters = new SchemaElementLookUpTable<Parameter>();
				}
				return this._parameters;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060023FC RID: 9212 RVA: 0x00065A23 File Offset: 0x00063C23
		// (set) Token: 0x060023FD RID: 9213 RVA: 0x00065A2B File Offset: 0x00063C2B
		public CollectionKind CollectionKind
		{
			get
			{
				return this._returnTypeCollectionKind;
			}
			internal set
			{
				this._returnTypeCollectionKind = value;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x00065A34 File Offset: 0x00063C34
		public override string Identity
		{
			get
			{
				if (string.IsNullOrEmpty(this._functionStrongName))
				{
					StringBuilder stringBuilder = new StringBuilder(this.FQName);
					bool flag = true;
					stringBuilder.Append('(');
					foreach (Parameter parameter in this.Parameters)
					{
						if (!flag)
						{
							stringBuilder.Append(',');
						}
						else
						{
							flag = false;
						}
						stringBuilder.Append(Helper.ToString(parameter.ParameterDirection));
						stringBuilder.Append(' ');
						parameter.WriteIdentity(stringBuilder);
					}
					stringBuilder.Append(')');
					this._functionStrongName = stringBuilder.ToString();
				}
				return this._functionStrongName;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x00065AF0 File Offset: 0x00063CF0
		public bool IsReturnAttributeReftype
		{
			get
			{
				return this._isRefType;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x00065AF8 File Offset: 0x00063CF8
		public virtual bool IsFunctionImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x00065AFB File Offset: 0x00063CFB
		public string DbSchema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x00065B04 File Offset: 0x00063D04
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "CommandText"))
			{
				this.HandleCommandTextFunctionElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Parameter"))
			{
				this.HandleParameterElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "ReturnType"))
			{
				this.HandleReturnTypeElement(reader);
				return true;
			}
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				if (base.CanHandleElement(reader, "ValueAnnotation"))
				{
					this.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "TypeAnnotation"))
				{
					this.SkipElement(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x00065BA0 File Offset: 0x00063DA0
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "ReturnType"))
			{
				this.HandleReturnTypeAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Aggregate"))
			{
				this.HandleAggregateAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "BuiltIn"))
			{
				this.HandleBuiltInAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "StoreFunctionName"))
			{
				this.HandleStoreFunctionNameAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "NiladicFunction"))
			{
				this.HandleNiladicFunctionAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "IsComposable"))
			{
				this.HandleIsComposableAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "ParameterTypeSemantics"))
			{
				this.HandleParameterTypeSemanticsAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Schema"))
			{
				this.HandleDbSchemaAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x00065C6C File Offset: 0x00063E6C
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (this._unresolvedType != null)
			{
				base.Schema.ResolveTypeName(this, this.UnresolvedReturnType, out this._type);
			}
			if (this._returnTypeList != null)
			{
				foreach (ReturnType returnType in this._returnTypeList)
				{
					returnType.ResolveTopLevelNames();
				}
			}
			foreach (Parameter parameter in this.Parameters)
			{
				parameter.ResolveTopLevelNames();
			}
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x00065D24 File Offset: 0x00063F24
		internal override void Validate()
		{
			base.Validate();
			if (this._type != null && this._returnTypeList != null)
			{
				base.AddError(ErrorCode.ReturnTypeDeclaredAsAttributeAndElement, EdmSchemaErrorSeverity.Error, Strings.TypeDeclaredAsAttributeAndElement);
			}
			if (this._returnTypeList == null && this.Type == null)
			{
				if (this.IsComposable)
				{
					base.AddError(ErrorCode.ComposableFunctionOrFunctionImportWithoutReturnType, EdmSchemaErrorSeverity.Error, Strings.ComposableFunctionOrFunctionImportMustDeclareReturnType);
				}
			}
			else if (!this.IsComposable && !this.IsFunctionImport)
			{
				base.AddError(ErrorCode.NonComposableFunctionWithReturnType, EdmSchemaErrorSeverity.Error, Strings.NonComposableFunctionMustNotDeclareReturnType);
			}
			if (base.Schema.DataModel != SchemaDataModelOption.EntityDataModel)
			{
				if (this.IsAggregate)
				{
					if (this.Parameters.Count == 0)
					{
						base.AddError(ErrorCode.InvalidNumberOfParametersForAggregateFunction, EdmSchemaErrorSeverity.Error, this, Strings.InvalidNumberOfParametersForAggregateFunction(this.FQName));
					}
					else if (this.Parameters.GetElementAt(0).CollectionKind == CollectionKind.None)
					{
						Parameter elementAt = this.Parameters.GetElementAt(0);
						base.AddError(ErrorCode.InvalidParameterTypeForAggregateFunction, EdmSchemaErrorSeverity.Error, this, Strings.InvalidParameterTypeForAggregateFunction(elementAt.Name, this.FQName));
					}
				}
				if (!this.IsComposable && (this.IsAggregate || this.IsNiladicFunction || this.IsBuiltIn))
				{
					base.AddError(ErrorCode.NonComposableFunctionAttributesNotValid, EdmSchemaErrorSeverity.Error, Strings.NonComposableFunctionHasDisallowedAttribute);
				}
				if (this.CommandText != null)
				{
					if (this.IsComposable)
					{
						base.AddError(ErrorCode.ComposableFunctionWithCommandText, EdmSchemaErrorSeverity.Error, Strings.CommandTextFunctionsNotComposable);
					}
					if (this.StoreFunctionName != null)
					{
						base.AddError(ErrorCode.FunctionDeclaresCommandTextAndStoreFunctionName, EdmSchemaErrorSeverity.Error, Strings.CommandTextFunctionsCannotDeclareStoreFunctionName);
					}
				}
			}
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel && this._type != null && (!(this._type is ScalarType) || this._returnTypeCollectionKind != CollectionKind.None))
			{
				base.AddError(ErrorCode.FunctionWithNonPrimitiveTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonPrimitiveTypeNotSupported(Function.GetTypeNameForErrorMessage(this._type, this._returnTypeCollectionKind, this._isRefType), this.FQName));
			}
			if (this._returnTypeList != null)
			{
				foreach (ReturnType returnType in this._returnTypeList)
				{
					returnType.Validate();
				}
			}
			if (this._parameters != null)
			{
				foreach (Parameter parameter in this._parameters)
				{
					parameter.Validate();
				}
			}
			if (this._commandText != null)
			{
				this._commandText.Validate();
			}
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x00065F84 File Offset: 0x00064184
		internal override void ResolveSecondLevelNames()
		{
			foreach (Parameter parameter in this._parameters)
			{
				parameter.ResolveSecondLevelNames();
			}
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x00065FD0 File Offset: 0x000641D0
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			throw Error.NotImplemented();
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x00065FD8 File Offset: 0x000641D8
		protected void CloneSetFunctionFields(Function clone)
		{
			clone._isAggregate = this._isAggregate;
			clone._isBuiltIn = this._isBuiltIn;
			clone._isNiladicFunction = this._isNiladicFunction;
			clone._isComposable = this._isComposable;
			clone._commandText = this._commandText;
			clone._storeFunctionName = this._storeFunctionName;
			clone._type = this._type;
			clone._returnTypeList = this._returnTypeList;
			clone._returnTypeCollectionKind = this._returnTypeCollectionKind;
			clone._parameterTypeSemantics = this._parameterTypeSemantics;
			clone._schema = this._schema;
			clone.Name = this.Name;
			foreach (Parameter parameter in this.Parameters)
			{
				clone.Parameters.TryAdd((Parameter)parameter.Clone(clone));
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x000660C8 File Offset: 0x000642C8
		// (set) Token: 0x0600240A RID: 9226 RVA: 0x000660D0 File Offset: 0x000642D0
		internal string UnresolvedReturnType
		{
			get
			{
				return this._unresolvedType;
			}
			set
			{
				this._unresolvedType = value;
			}
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000660D9 File Offset: 0x000642D9
		private void HandleDbSchemaAttribute(XmlReader reader)
		{
			this._schema = reader.Value;
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000660E8 File Offset: 0x000642E8
		private void HandleAggregateAttribute(XmlReader reader)
		{
			bool flag = false;
			base.HandleBoolAttribute(reader, ref flag);
			this.IsAggregate = flag;
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x00066108 File Offset: 0x00064308
		private void HandleBuiltInAttribute(XmlReader reader)
		{
			bool flag = false;
			base.HandleBoolAttribute(reader, ref flag);
			this.IsBuiltIn = flag;
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x00066128 File Offset: 0x00064328
		private void HandleStoreFunctionNameAttribute(XmlReader reader)
		{
			string text = reader.Value;
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
				this.StoreFunctionName = text;
			}
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x00066154 File Offset: 0x00064354
		private void HandleNiladicFunctionAttribute(XmlReader reader)
		{
			bool flag = false;
			base.HandleBoolAttribute(reader, ref flag);
			this.IsNiladicFunction = flag;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x00066174 File Offset: 0x00064374
		private void HandleIsComposableAttribute(XmlReader reader)
		{
			bool flag = true;
			base.HandleBoolAttribute(reader, ref flag);
			this.IsComposable = flag;
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x00066194 File Offset: 0x00064394
		private void HandleCommandTextFunctionElement(XmlReader reader)
		{
			FunctionCommandText functionCommandText = new FunctionCommandText(this);
			functionCommandText.Parse(reader);
			this._commandText = functionCommandText;
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000661B8 File Offset: 0x000643B8
		protected virtual void HandleReturnTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetString(base.Schema, reader, out text))
			{
				return;
			}
			TypeModifier typeModifier;
			Function.RemoveTypeModifier(ref text, out typeModifier, out this._isRefType);
			if (typeModifier != TypeModifier.None && typeModifier == TypeModifier.Array)
			{
				this.CollectionKind = CollectionKind.Bag;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			this.UnresolvedReturnType = text;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x0006620C File Offset: 0x0006440C
		protected void HandleParameterElement(XmlReader reader)
		{
			Parameter parameter = new Parameter(this);
			parameter.Parse(reader);
			this.Parameters.Add(parameter, true, new Func<object, string>(Strings.ParameterNameAlreadyDefinedDuplicate));
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x00066240 File Offset: 0x00064440
		protected void HandleReturnTypeElement(XmlReader reader)
		{
			ReturnType returnType = new ReturnType(this);
			returnType.Parse(reader);
			if (this._returnTypeList == null)
			{
				this._returnTypeList = new List<ReturnType>();
			}
			this._returnTypeList.Add(returnType);
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x0006627C File Offset: 0x0006447C
		private void HandleParameterTypeSemanticsAttribute(XmlReader reader)
		{
			string text = reader.Value;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			text = text.Trim();
			if (!string.IsNullOrEmpty(text))
			{
				if (text != null)
				{
					if (text == "ExactMatchOnly")
					{
						this.ParameterTypeSemantics = ParameterTypeSemantics.ExactMatchOnly;
						return;
					}
					if (text == "AllowImplicitPromotion")
					{
						this.ParameterTypeSemantics = ParameterTypeSemantics.AllowImplicitPromotion;
						return;
					}
					if (text == "AllowImplicitConversion")
					{
						this.ParameterTypeSemantics = ParameterTypeSemantics.AllowImplicitConversion;
						return;
					}
				}
				base.AddError(ErrorCode.InvalidValueForParameterTypeSemantics, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidValueForParameterTypeSemanticsAttribute(text));
			}
		}

		// Token: 0x04000CD1 RID: 3281
		private bool _isAggregate;

		// Token: 0x04000CD2 RID: 3282
		private bool _isBuiltIn;

		// Token: 0x04000CD3 RID: 3283
		private bool _isNiladicFunction;

		// Token: 0x04000CD4 RID: 3284
		protected bool _isComposable = true;

		// Token: 0x04000CD5 RID: 3285
		protected FunctionCommandText _commandText;

		// Token: 0x04000CD6 RID: 3286
		private string _storeFunctionName;

		// Token: 0x04000CD7 RID: 3287
		protected SchemaType _type;

		// Token: 0x04000CD8 RID: 3288
		private string _unresolvedType;

		// Token: 0x04000CD9 RID: 3289
		protected bool _isRefType;

		// Token: 0x04000CDA RID: 3290
		protected SchemaElementLookUpTable<Parameter> _parameters;

		// Token: 0x04000CDB RID: 3291
		protected List<ReturnType> _returnTypeList;

		// Token: 0x04000CDC RID: 3292
		private CollectionKind _returnTypeCollectionKind;

		// Token: 0x04000CDD RID: 3293
		private ParameterTypeSemantics _parameterTypeSemantics;

		// Token: 0x04000CDE RID: 3294
		private string _schema;

		// Token: 0x04000CDF RID: 3295
		private string _functionStrongName;

		// Token: 0x04000CE0 RID: 3296
		private static readonly Regex _typeParser = new Regex("^(?<modifier>((Collection)|(Ref)))\\s*\\(\\s*(?<typeName>\\S*)\\s*\\)$", RegexOptions.Compiled);
	}
}
