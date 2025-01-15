using System;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000029 RID: 41
	internal class Function : SchemaType
	{
		// Token: 0x06000682 RID: 1666 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		internal static void RemoveTypeModifier(ref string type, out TypeModifier typeModifier, out bool isRefType)
		{
			isRefType = false;
			typeModifier = TypeModifier.None;
			Match match = Function.s_typeParser.Match(type);
			if (!match.Success)
			{
				return;
			}
			type = match.Groups["typeName"].Value;
			string value = match.Groups["modifier"].Value;
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
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0000BB5C File Offset: 0x00009D5C
		public Function(Schema parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0000BB6C File Offset: 0x00009D6C
		protected Function(EntityContainer container)
			: base(container.Schema)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
			this._container = container;
			this._isComposable = false;
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0000BBBD File Offset: 0x00009DBD
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x0000BBC5 File Offset: 0x00009DC5
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

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0000BBCE File Offset: 0x00009DCE
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0000BBD6 File Offset: 0x00009DD6
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

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0000BBDF File Offset: 0x00009DDF
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x0000BBE7 File Offset: 0x00009DE7
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

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0000BBF0 File Offset: 0x00009DF0
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0000BBF8 File Offset: 0x00009DF8
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

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0000BC01 File Offset: 0x00009E01
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

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0000BC18 File Offset: 0x00009E18
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x0000BC20 File Offset: 0x00009E20
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

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0000BC29 File Offset: 0x00009E29
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x0000BC31 File Offset: 0x00009E31
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

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0000BC3A File Offset: 0x00009E3A
		public virtual SchemaType Type
		{
			get
			{
				if (this.ReturnType != null)
				{
					return this.ReturnType.Type;
				}
				return this._type;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0000BC56 File Offset: 0x00009E56
		public ReturnType ReturnType
		{
			get
			{
				return this._returnType;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0000BC5E File Offset: 0x00009E5E
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

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000BC79 File Offset: 0x00009E79
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0000BC81 File Offset: 0x00009E81
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

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0000BC8C File Offset: 0x00009E8C
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

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0000BD48 File Offset: 0x00009F48
		public bool IsReturnAttributeReftype
		{
			get
			{
				return this._isRefType;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0000BD50 File Offset: 0x00009F50
		public virtual bool IsFunctionImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0000BD53 File Offset: 0x00009F53
		public EntityContainerEntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0000BD5B File Offset: 0x00009F5B
		public string DbSchema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0000BD64 File Offset: 0x00009F64
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "CommandText"))
			{
				this.HandleCommandTextFunctionElment(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Parameter"))
			{
				this.HandleParameterElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "ReturnType"))
			{
				if (base.ParentElement.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel || base.ParentElement.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
				{
					this.HandleReturnTypeElement(reader);
				}
				else
				{
					this.SkipThroughElement(reader);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0000BDF0 File Offset: 0x00009FF0
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
				this.HandleIsComposableFunctionAttribute(reader);
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
			if (SchemaElement.CanHandleAttribute(reader, "EntitySet"))
			{
				this.HandleEntitySetAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (this.ReturnType != null)
			{
				this.ReturnType.ResolveTopLevelNames();
			}
			else if (this.UnresolvedReturnType != null && base.Schema.ResolveTypeName(this, this.UnresolvedReturnType, out this._type))
			{
				if (this.IsFunctionImport)
				{
					if (!this.MeetsFunctionImportReturnTypeRequirements(this._type))
					{
						base.AddError(ErrorCode.FunctionImportUnsupportedReturnType, EdmSchemaErrorSeverity.Error, this, this.GetReturnTypeErrorMessage(base.Schema.SchemaVersion, this.Name));
					}
				}
				else if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel && !(this._type is ScalarType))
				{
					base.AddError(ErrorCode.FunctionWithNonScalarTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonPrimitiveTypeNotSupported(this._type.FQName, this.FQName));
				}
			}
			if (this.IsFunctionImport && this._entitySet == null && this._unresolvedEntitySet != null)
			{
				this._entitySet = this._container.FindEntitySet(this._unresolvedEntitySet);
				if (this._entitySet == null)
				{
					base.AddError(ErrorCode.FunctionImportUnknownEntitySet, EdmSchemaErrorSeverity.Error, Strings.FunctionImportUnknownEntitySet(this._unresolvedEntitySet, this.Name));
				}
			}
			foreach (Parameter parameter in this.Parameters)
			{
				parameter.ResolveTopLevelNames();
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0000C028 File Offset: 0x0000A228
		private bool MeetsFunctionImportReturnTypeRequirements(SchemaType type)
		{
			if (this._type is ScalarType && this._returnTypeCollectionKind == CollectionKind.Bag)
			{
				return true;
			}
			if (this._type is SchemaEntityType && this._returnTypeCollectionKind == CollectionKind.Bag)
			{
				return true;
			}
			if (base.Schema.SchemaVersion == 1.1)
			{
				if (this._type is ScalarType && this._returnTypeCollectionKind == CollectionKind.None)
				{
					return true;
				}
				if (this._type is SchemaEntityType && this._returnTypeCollectionKind == CollectionKind.None)
				{
					return true;
				}
				if (this._type is SchemaComplexType && this._returnTypeCollectionKind == CollectionKind.None)
				{
					return true;
				}
				if (this._type is SchemaComplexType && this._returnTypeCollectionKind == CollectionKind.Bag)
				{
					return true;
				}
			}
			return base.Schema.SchemaVersion == 2.0 && this._type is SchemaComplexType && this._returnTypeCollectionKind == CollectionKind.Bag;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0000C108 File Offset: 0x0000A308
		private string GetReturnTypeErrorMessage(double schemaVersion, string functionName)
		{
			string text;
			if (base.Schema.SchemaVersion == 1.0)
			{
				text = Strings.FunctionImportWithUnsupportedReturnTypeV1(functionName);
			}
			else if (base.Schema.SchemaVersion == 1.1)
			{
				text = Strings.FunctionImportWithUnsupportedReturnTypeV1_1(functionName);
			}
			else
			{
				text = Strings.FunctionImportWithUnsupportedReturnTypeV2(functionName);
			}
			return text;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000C15C File Offset: 0x0000A35C
		internal override void Validate()
		{
			base.Validate();
			if (this._returnType != null && this._type != null)
			{
				base.AddError(ErrorCode.ReturnTypeDeclaredAsAttributeAndElement, EdmSchemaErrorSeverity.Error, Strings.ReturnTypeDeclaredAsAttributeAndElement);
			}
			if (this._commandText != null)
			{
				this._commandText.Validate();
			}
			if (base.Schema.DataModel != SchemaDataModelOption.EntityDataModel)
			{
				if (this.Type == null)
				{
					if (this.IsComposable)
					{
						base.AddError(ErrorCode.ComposableFunctionWithoutReturnType, EdmSchemaErrorSeverity.Error, Strings.ComposableFunctionMustDeclareReturnType);
					}
				}
				else if (!this.IsComposable && !this.IsFunctionImport)
				{
					base.AddError(ErrorCode.NonComposableFunctionWithReturnType, EdmSchemaErrorSeverity.Error, Strings.NonComposableFunctionMustNotDeclareReturnType);
				}
				if (this.IsAggregate)
				{
					if (this.Parameters.Count != 1)
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
			if (this.IsFunctionImport)
			{
				this.ValidateReturnType(this.Type);
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		internal override void ResolveSecondLevelNames()
		{
			foreach (Parameter parameter in this._parameters)
			{
				parameter.ResolveSecondLevelNames();
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000C330 File Offset: 0x0000A530
		private void ValidateReturnType(SchemaType returnType)
		{
			SchemaEntityType schemaEntityType = returnType as SchemaEntityType;
			if (schemaEntityType != null)
			{
				if (this.EntitySet == null)
				{
					base.AddError(ErrorCode.FunctionImportReturnsEntitiesButDoesNotSpecifyEntitySet, EdmSchemaErrorSeverity.Error, Strings.FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet(this.Name));
					return;
				}
				if (this.EntitySet.EntityType != null && !schemaEntityType.IsOfType(this.EntitySet.EntityType))
				{
					base.AddError(ErrorCode.FunctionImportEntityTypeDoesNotMatchEntitySet, EdmSchemaErrorSeverity.Error, Strings.FunctionImportEntityTypeDoesNotMatchEntitySet(this.FQName, this.EntitySet.EntityType.FQName, this.EntitySet.Name, base.ParentElement.FQName));
					return;
				}
			}
			else
			{
				SchemaComplexType schemaComplexType = returnType as SchemaComplexType;
				if (schemaComplexType != null)
				{
					if (this.EntitySet != null)
					{
						base.AddError(ErrorCode.ComplexTypeAsReturnTypeAndDefinedEntitySet, EdmSchemaErrorSeverity.Error, base.LineNumber, base.LinePosition, Strings.ComplexTypeAsReturnTypeAndDefinedEntitySet(this.Name, schemaComplexType.Name, this.EntitySet.Name));
						return;
					}
				}
				else if (this.EntitySet != null)
				{
					base.AddError(ErrorCode.FunctionImportSpecifiesEntitySetButDoesNotReturnEntityType, EdmSchemaErrorSeverity.Error, Strings.FunctionImportSpecifiesEntitySetButNotEntityType(this.Name, base.ParentElement.FQName));
				}
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0000C440 File Offset: 0x0000A640
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			Function function = new FunctionImportElement((EntityContainer)parentElement);
			function._isAggregate = this._isAggregate;
			function._isBuiltIn = this._isBuiltIn;
			function._isNiladicFunction = this._isNiladicFunction;
			function._isComposable = this._isComposable;
			function._entitySet = this._entitySet;
			function._commandText = this._commandText;
			function._storeFunctionName = this._storeFunctionName;
			function._type = this._type;
			function._returnType = this._returnType;
			function._returnTypeCollectionKind = this._returnTypeCollectionKind;
			function._parameterTypeSemantics = this._parameterTypeSemantics;
			function._schema = this._schema;
			function.Name = this.Name;
			foreach (Parameter parameter in this.Parameters)
			{
				function.Parameters.TryAdd((Parameter)parameter.Clone(function));
			}
			return function;
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0000C548 File Offset: 0x0000A748
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0000C550 File Offset: 0x0000A750
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

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000C559 File Offset: 0x0000A759
		private void HandleDbSchemaAttribute(XmlReader reader)
		{
			this._schema = reader.Value;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000C568 File Offset: 0x0000A768
		private void HandleAggregateAttribute(XmlReader reader)
		{
			bool flag = false;
			base.HandleBoolAttribute(reader, ref flag);
			this.IsAggregate = flag;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000C588 File Offset: 0x0000A788
		private void HandleBuiltInAttribute(XmlReader reader)
		{
			bool flag = false;
			base.HandleBoolAttribute(reader, ref flag);
			this.IsBuiltIn = flag;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		private void HandleStoreFunctionNameAttribute(XmlReader reader)
		{
			string text = reader.Value.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
				this.StoreFunctionName = text;
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		private void HandleNiladicFunctionAttribute(XmlReader reader)
		{
			bool flag = false;
			base.HandleBoolAttribute(reader, ref flag);
			this.IsNiladicFunction = flag;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		private void HandleIsComposableFunctionAttribute(XmlReader reader)
		{
			bool flag = true;
			base.HandleBoolAttribute(reader, ref flag);
			this.IsComposable = flag;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000C618 File Offset: 0x0000A818
		private void HandleCommandTextFunctionElment(XmlReader reader)
		{
			FunctionCommandText functionCommandText = new FunctionCommandText(this);
			functionCommandText.Parse(reader);
			this._commandText = functionCommandText;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000C63C File Offset: 0x0000A83C
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

		// Token: 0x060006AF RID: 1711 RVA: 0x0000C690 File Offset: 0x0000A890
		protected void HandleParameterElement(XmlReader reader)
		{
			Parameter parameter = new Parameter(this);
			parameter.Parse(reader);
			SchemaElementLookUpTable<Parameter> parameters = this.Parameters;
			Parameter parameter2 = parameter;
			bool flag = true;
			Func<object, string> func;
			if ((func = Function.<>O.<0>__ParameterNameAlreadyDefinedDuplicate) == null)
			{
				func = (Function.<>O.<0>__ParameterNameAlreadyDefinedDuplicate = new Func<object, string>(Strings.ParameterNameAlreadyDefinedDuplicate));
			}
			parameters.Add(parameter2, flag, func);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		protected void HandleReturnTypeElement(XmlReader reader)
		{
			ReturnType returnType = new ReturnType(this);
			returnType.Parse(reader);
			this._returnType = returnType;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
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
				base.AddError(ErrorCode.InvalidValueForParameterTypeSemantics, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidValueForParameterTypeSemanticsAttribute(text));
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000C778 File Offset: 0x0000A978
		private void HandleEntitySetAttribute(XmlReader reader)
		{
			string text;
			if (Utils.GetString(base.Schema, reader, out text))
			{
				this._unresolvedEntitySet = text;
			}
		}

		// Token: 0x0400064F RID: 1615
		private bool _isAggregate;

		// Token: 0x04000650 RID: 1616
		private bool _isBuiltIn;

		// Token: 0x04000651 RID: 1617
		private bool _isNiladicFunction;

		// Token: 0x04000652 RID: 1618
		protected bool _isComposable = true;

		// Token: 0x04000653 RID: 1619
		private string _unresolvedEntitySet;

		// Token: 0x04000654 RID: 1620
		protected FunctionCommandText _commandText;

		// Token: 0x04000655 RID: 1621
		private string _storeFunctionName;

		// Token: 0x04000656 RID: 1622
		protected SchemaType _type;

		// Token: 0x04000657 RID: 1623
		private string _unresolvedType;

		// Token: 0x04000658 RID: 1624
		protected bool _isRefType;

		// Token: 0x04000659 RID: 1625
		protected SchemaElementLookUpTable<Parameter> _parameters;

		// Token: 0x0400065A RID: 1626
		protected ReturnType _returnType;

		// Token: 0x0400065B RID: 1627
		private CollectionKind _returnTypeCollectionKind;

		// Token: 0x0400065C RID: 1628
		private ParameterTypeSemantics _parameterTypeSemantics;

		// Token: 0x0400065D RID: 1629
		private EntityContainer _container;

		// Token: 0x0400065E RID: 1630
		private EntityContainerEntitySet _entitySet;

		// Token: 0x0400065F RID: 1631
		private string _schema;

		// Token: 0x04000660 RID: 1632
		private string _functionStrongName;

		// Token: 0x04000661 RID: 1633
		private static Regex s_typeParser = new Regex("^(?<modifier>((Collection)|(Ref)))\\s*\\(\\s*(?<typeName>\\S*)\\s*\\)$");

		// Token: 0x0200029A RID: 666
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000F6B RID: 3947
			public static Func<object, string> <0>__ParameterNameAlreadyDefinedDuplicate;
		}
	}
}
