using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Xml;
using System.Xml.Schema;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000310 RID: 784
	[DebuggerDisplay("Namespace={Namespace}, PublicKeyToken={PublicKeyToken}, Version={Version}")]
	internal class Schema : SchemaElement
	{
		// Token: 0x0600252D RID: 9517 RVA: 0x00069B7D File Offset: 0x00067D7D
		public Schema(SchemaManager schemaManager)
			: base(null, null)
		{
			this._schemaManager = schemaManager;
			this._errors = new List<EdmSchemaError>();
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x00069BA4 File Offset: 0x00067DA4
		internal IList<EdmSchemaError> Resolve()
		{
			this.ResolveTopLevelNames();
			if (this._errors.Count != 0)
			{
				return this.ResetErrors();
			}
			this.ResolveSecondLevelNames();
			return this.ResetErrors();
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x00069BCC File Offset: 0x00067DCC
		internal IList<EdmSchemaError> ValidateSchema()
		{
			this.Validate();
			return this.ResetErrors();
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x00069BDA File Offset: 0x00067DDA
		internal void AddError(EdmSchemaError error)
		{
			this._errors.Add(error);
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x00069BE8 File Offset: 0x00067DE8
		internal IList<EdmSchemaError> Parse(XmlReader sourceReader, string sourceLocation)
		{
			try
			{
				XmlReaderSettings xmlReaderSettings = this.CreateXmlReaderSettings();
				XmlReader xmlReader = XmlReader.Create(sourceReader, xmlReaderSettings);
				return this.InternalParse(xmlReader, sourceLocation);
			}
			catch (IOException ex)
			{
				base.AddError(ErrorCode.IOException, EdmSchemaErrorSeverity.Error, sourceReader, ex);
			}
			return this.ResetErrors();
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x00069C34 File Offset: 0x00067E34
		private IList<EdmSchemaError> InternalParse(XmlReader sourceReader, string sourceLocation)
		{
			base.Schema = this;
			this.Location = sourceLocation;
			try
			{
				if (sourceReader.NodeType != XmlNodeType.Element)
				{
					while (sourceReader.Read() && sourceReader.NodeType != XmlNodeType.Element)
					{
					}
				}
				base.GetPositionInfo(sourceReader);
				List<string> primarySchemaNamespaces = Schema.SomSchemaSetHelper.GetPrimarySchemaNamespaces(this.DataModel);
				if (sourceReader.EOF)
				{
					if (sourceLocation != null)
					{
						base.AddError(ErrorCode.EmptyFile, EdmSchemaErrorSeverity.Error, Strings.EmptyFile(sourceLocation));
					}
					else
					{
						base.AddError(ErrorCode.EmptyFile, EdmSchemaErrorSeverity.Error, Strings.EmptySchemaTextReader);
					}
				}
				else if (!primarySchemaNamespaces.Contains(sourceReader.NamespaceURI))
				{
					Func<object, object, object, string> func = new Func<object, object, object, string>(Strings.UnexpectedRootElement);
					if (string.IsNullOrEmpty(sourceReader.NamespaceURI))
					{
						func = new Func<object, object, object, string>(Strings.UnexpectedRootElementNoNamespace);
					}
					string commaDelimitedString = Helper.GetCommaDelimitedString(primarySchemaNamespaces);
					base.AddError(ErrorCode.UnexpectedXmlElement, EdmSchemaErrorSeverity.Error, func(sourceReader.NamespaceURI, sourceReader.LocalName, commaDelimitedString));
				}
				else
				{
					this.SchemaXmlNamespace = sourceReader.NamespaceURI;
					if (this.DataModel == SchemaDataModelOption.EntityDataModel)
					{
						if (this.SchemaXmlNamespace == "http://schemas.microsoft.com/ado/2006/04/edm")
						{
							this.SchemaVersion = 1.0;
						}
						else if (this.SchemaXmlNamespace == "http://schemas.microsoft.com/ado/2007/05/edm")
						{
							this.SchemaVersion = 1.1;
						}
						else if (this.SchemaXmlNamespace == "http://schemas.microsoft.com/ado/2008/09/edm")
						{
							this.SchemaVersion = 2.0;
						}
						else
						{
							this.SchemaVersion = 3.0;
						}
					}
					else if (this.DataModel == SchemaDataModelOption.ProviderDataModel)
					{
						if (this.SchemaXmlNamespace == "http://schemas.microsoft.com/ado/2006/04/edm/ssdl")
						{
							this.SchemaVersion = 1.0;
						}
						else if (this.SchemaXmlNamespace == "http://schemas.microsoft.com/ado/2009/02/edm/ssdl")
						{
							this.SchemaVersion = 2.0;
						}
						else
						{
							this.SchemaVersion = 3.0;
						}
					}
					string localName = sourceReader.LocalName;
					if (localName != null && (localName == "Schema" || localName == "ProviderManifest"))
					{
						this.HandleTopLevelSchemaElement(sourceReader);
						sourceReader.Read();
					}
					else
					{
						base.AddError(ErrorCode.UnexpectedXmlElement, EdmSchemaErrorSeverity.Error, Strings.UnexpectedRootElement(sourceReader.NamespaceURI, sourceReader.LocalName, this.SchemaXmlNamespace));
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				base.AddError(ErrorCode.InternalError, EdmSchemaErrorSeverity.Error, ex.Message);
			}
			catch (UnauthorizedAccessException ex2)
			{
				base.AddError(ErrorCode.UnauthorizedAccessException, EdmSchemaErrorSeverity.Error, sourceReader, ex2);
			}
			catch (IOException ex3)
			{
				base.AddError(ErrorCode.IOException, EdmSchemaErrorSeverity.Error, sourceReader, ex3);
			}
			catch (SecurityException ex4)
			{
				base.AddError(ErrorCode.SecurityError, EdmSchemaErrorSeverity.Error, sourceReader, ex4);
			}
			catch (XmlException ex5)
			{
				base.AddError(ErrorCode.XmlError, EdmSchemaErrorSeverity.Error, sourceReader, ex5);
			}
			return this.ResetErrors();
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x00069F2C File Offset: 0x0006812C
		internal static XmlReaderSettings CreateEdmStandardXmlReaderSettings()
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.CheckCharacters = true;
			xmlReaderSettings.CloseInput = false;
			xmlReaderSettings.IgnoreWhitespace = true;
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
			xmlReaderSettings.IgnoreComments = true;
			xmlReaderSettings.IgnoreProcessingInstructions = true;
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.ValidationFlags &= ~XmlSchemaValidationFlags.ProcessIdentityConstraints;
			xmlReaderSettings.ValidationFlags &= ~XmlSchemaValidationFlags.ProcessSchemaLocation;
			xmlReaderSettings.ValidationFlags &= ~XmlSchemaValidationFlags.ProcessInlineSchema;
			return xmlReaderSettings;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00069F9C File Offset: 0x0006819C
		private XmlReaderSettings CreateXmlReaderSettings()
		{
			XmlReaderSettings xmlReaderSettings = Schema.CreateEdmStandardXmlReaderSettings();
			xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
			xmlReaderSettings.ValidationEventHandler += this.OnSchemaValidationEvent;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			XmlSchemaSet schemaSet = Schema.SomSchemaSetHelper.GetSchemaSet(this.DataModel);
			xmlReaderSettings.Schemas = schemaSet;
			return xmlReaderSettings;
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x00069FE8 File Offset: 0x000681E8
		internal void OnSchemaValidationEvent(object sender, ValidationEventArgs e)
		{
			XmlReader xmlReader = sender as XmlReader;
			if (xmlReader != null && !this.IsValidateableXmlNamespace(xmlReader.NamespaceURI, xmlReader.NodeType == XmlNodeType.Attribute))
			{
				if (this.SchemaVersion == 1.0 || this.SchemaVersion == 1.1)
				{
					return;
				}
				if (xmlReader.NodeType == XmlNodeType.Attribute || e.Severity == XmlSeverityType.Warning)
				{
					return;
				}
			}
			if (this.SchemaVersion >= 2.0 && xmlReader.NodeType == XmlNodeType.Attribute && e.Severity == XmlSeverityType.Warning)
			{
				return;
			}
			EdmSchemaErrorSeverity edmSchemaErrorSeverity = EdmSchemaErrorSeverity.Error;
			if (e.Severity == XmlSeverityType.Warning)
			{
				edmSchemaErrorSeverity = EdmSchemaErrorSeverity.Warning;
			}
			base.AddError(ErrorCode.XmlError, edmSchemaErrorSeverity, e.Exception.LineNumber, e.Exception.LinePosition, e.Message);
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x0006A0A4 File Offset: 0x000682A4
		public bool IsParseableXmlNamespace(string xmlNamespaceUri, bool isAttribute)
		{
			if (string.IsNullOrEmpty(xmlNamespaceUri) && isAttribute)
			{
				return true;
			}
			if (this._parseableXmlNamespaces == null)
			{
				this._parseableXmlNamespaces = new HashSet<string>();
				foreach (XmlSchemaResource xmlSchemaResource in XmlSchemaResource.GetMetadataSchemaResourceMap(this.SchemaVersion).Values)
				{
					this._parseableXmlNamespaces.Add(xmlSchemaResource.NamespaceUri);
				}
			}
			return this._parseableXmlNamespaces.Contains(xmlNamespaceUri);
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x0006A138 File Offset: 0x00068338
		public bool IsValidateableXmlNamespace(string xmlNamespaceUri, bool isAttribute)
		{
			if (string.IsNullOrEmpty(xmlNamespaceUri) && isAttribute)
			{
				return true;
			}
			if (this._validatableXmlNamespaces == null)
			{
				HashSet<string> hashSet = new HashSet<string>();
				foreach (XmlSchemaResource xmlSchemaResource in XmlSchemaResource.GetMetadataSchemaResourceMap((this.SchemaVersion == 0.0) ? 3.0 : this.SchemaVersion).Values)
				{
					Schema.AddAllSchemaResourceNamespaceNames(hashSet, xmlSchemaResource);
				}
				if (this.SchemaVersion == 0.0)
				{
					return hashSet.Contains(xmlNamespaceUri);
				}
				this._validatableXmlNamespaces = hashSet;
			}
			return this._validatableXmlNamespaces.Contains(xmlNamespaceUri);
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x0006A1FC File Offset: 0x000683FC
		private static void AddAllSchemaResourceNamespaceNames(HashSet<string> hashSet, XmlSchemaResource schemaResource)
		{
			hashSet.Add(schemaResource.NamespaceUri);
			foreach (XmlSchemaResource xmlSchemaResource in schemaResource.ImportedSchemas)
			{
				Schema.AddAllSchemaResourceNamespaceNames(hashSet, xmlSchemaResource);
			}
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x0006A23C File Offset: 0x0006843C
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			this.AliasResolver.ResolveNamespaces();
			foreach (SchemaType schemaType in this.SchemaTypes)
			{
				schemaType.ResolveTopLevelNames();
			}
			foreach (Function function in this.Functions)
			{
				function.ResolveTopLevelNames();
			}
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x0006A2DC File Offset: 0x000684DC
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			foreach (SchemaType schemaType in this.SchemaTypes)
			{
				schemaType.ResolveSecondLevelNames();
			}
			foreach (Function function in this.Functions)
			{
				function.ResolveSecondLevelNames();
			}
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x0006A374 File Offset: 0x00068574
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.Namespace))
			{
				base.AddError(ErrorCode.MissingNamespaceAttribute, EdmSchemaErrorSeverity.Error, Strings.MissingNamespaceAttribute);
				return;
			}
			if (!string.IsNullOrEmpty(this.Alias) && EdmItemCollection.IsSystemNamespace(this.ProviderManifest, this.Alias))
			{
				base.AddError(ErrorCode.CannotUseSystemNamespaceAsAlias, EdmSchemaErrorSeverity.Error, Strings.CannotUseSystemNamespaceAsAlias(this.Alias));
			}
			if (this.ProviderManifest != null && EdmItemCollection.IsSystemNamespace(this.ProviderManifest, this.Namespace))
			{
				base.AddError(ErrorCode.SystemNamespace, EdmSchemaErrorSeverity.Error, Strings.SystemNamespaceEncountered(this.Namespace));
			}
			foreach (SchemaType schemaType in this.SchemaTypes)
			{
				schemaType.Validate();
			}
			foreach (Function function in this.Functions)
			{
				this.AddFunctionType(function);
				function.Validate();
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x0006A494 File Offset: 0x00068694
		// (set) Token: 0x0600253D RID: 9533 RVA: 0x0006A49C File Offset: 0x0006869C
		internal string SchemaXmlNamespace { get; private set; }

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x0006A4A5 File Offset: 0x000686A5
		internal DbProviderManifest ProviderManifest
		{
			get
			{
				return this._schemaManager.GetProviderManifest(delegate(string message, ErrorCode code, EdmSchemaErrorSeverity severity)
				{
					base.AddError(code, severity, message);
				});
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x0006A4BE File Offset: 0x000686BE
		// (set) Token: 0x06002540 RID: 9536 RVA: 0x0006A4C6 File Offset: 0x000686C6
		internal double SchemaVersion
		{
			get
			{
				return this._schemaVersion;
			}
			set
			{
				this._schemaVersion = value;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002541 RID: 9537 RVA: 0x0006A4CF File Offset: 0x000686CF
		// (set) Token: 0x06002542 RID: 9538 RVA: 0x0006A4D7 File Offset: 0x000686D7
		internal virtual string Alias { get; private set; }

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002543 RID: 9539 RVA: 0x0006A4E0 File Offset: 0x000686E0
		// (set) Token: 0x06002544 RID: 9540 RVA: 0x0006A4E8 File Offset: 0x000686E8
		internal virtual string Namespace
		{
			get
			{
				return this._namespaceName;
			}
			private set
			{
				this._namespaceName = value;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x0006A4F1 File Offset: 0x000686F1
		// (set) Token: 0x06002546 RID: 9542 RVA: 0x0006A4F9 File Offset: 0x000686F9
		internal string Location
		{
			get
			{
				return this._location;
			}
			private set
			{
				this._location = value;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x0006A504 File Offset: 0x00068704
		internal MetadataProperty SchemaSource
		{
			get
			{
				if (this._schemaSourceProperty == null)
				{
					this._schemaSourceProperty = new MetadataProperty("SchemaSource", EdmProviderManifest.Instance.GetPrimitiveType(PrimitiveTypeKind.String), false, (this._location != null) ? this._location : string.Empty);
				}
				return this._schemaSourceProperty;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x0006A551 File Offset: 0x00068751
		internal List<SchemaType> SchemaTypes
		{
			get
			{
				if (this._schemaTypes == null)
				{
					this._schemaTypes = new List<SchemaType>();
				}
				return this._schemaTypes;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x0006A56C File Offset: 0x0006876C
		public override string FQName
		{
			get
			{
				return this.Namespace;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600254A RID: 9546 RVA: 0x0006A574 File Offset: 0x00068774
		private List<Function> Functions
		{
			get
			{
				if (this._functions == null)
				{
					this._functions = new List<Function>();
				}
				return this._functions;
			}
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x0006A590 File Offset: 0x00068790
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "EntityType"))
			{
				this.HandleEntityTypeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "ComplexType"))
			{
				this.HandleInlineTypeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Association"))
			{
				this.HandleAssociationElement(reader);
				return true;
			}
			if (this.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				if (base.CanHandleElement(reader, "Using"))
				{
					this.HandleUsingElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "Function"))
				{
					this.HandleModelFunctionElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "EnumType"))
				{
					this.HandleEnumTypeElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "ValueTerm"))
				{
					this.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "Annotations"))
				{
					this.SkipElement(reader);
					return true;
				}
			}
			if (this.DataModel == SchemaDataModelOption.EntityDataModel || this.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				if (base.CanHandleElement(reader, "EntityContainer"))
				{
					this.HandleEntityContainerTypeElement(reader);
					return true;
				}
				if (this.DataModel == SchemaDataModelOption.ProviderDataModel && base.CanHandleElement(reader, "Function"))
				{
					this.HandleFunctionElement(reader);
					return true;
				}
			}
			else
			{
				if (base.CanHandleElement(reader, "Types"))
				{
					this.SkipThroughElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "Functions"))
				{
					this.SkipThroughElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "Function"))
				{
					this.HandleFunctionElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "Type"))
				{
					this.HandleTypeInformationElement(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x0006A70D File Offset: 0x0006890D
		protected override bool ProhibitAttribute(string namespaceUri, string localName)
		{
			if (base.ProhibitAttribute(namespaceUri, localName))
			{
				return true;
			}
			if (namespaceUri == null)
			{
				localName == "Name";
				return false;
			}
			return false;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x0006A730 File Offset: 0x00068930
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (this._depth == 1)
			{
				return false;
			}
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Alias"))
			{
				this.HandleAliasAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Namespace"))
			{
				this.HandleNamespaceAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Provider"))
			{
				this.HandleProviderAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "ProviderManifestToken"))
			{
				this.HandleProviderManifestTokenAttribute(reader);
				return true;
			}
			if (reader.NamespaceURI == "http://schemas.microsoft.com/ado/2009/02/edm/annotation" && reader.LocalName == "UseStrongSpatialTypes")
			{
				this.HandleUseStrongSpatialTypesAnnotation(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x0006A7D9 File Offset: 0x000689D9
		protected override void HandleAttributesComplete()
		{
			if (this._depth < 2)
			{
				return;
			}
			if (this._depth == 2)
			{
				this._schemaManager.EnsurePrimitiveSchemaIsLoaded(this.SchemaVersion);
			}
			base.HandleAttributesComplete();
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x0006A808 File Offset: 0x00068A08
		protected override void SkipThroughElement(XmlReader reader)
		{
			try
			{
				this._depth++;
				base.SkipThroughElement(reader);
			}
			finally
			{
				this._depth--;
			}
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x0006A84C File Offset: 0x00068A4C
		internal bool ResolveTypeName(SchemaElement usingElement, string typeName, out SchemaType type)
		{
			type = null;
			string text;
			string text2;
			Utils.ExtractNamespaceAndName(typeName, out text, out text2);
			string text3 = text;
			if (text3 == null)
			{
				text3 = ((this.ProviderManifest == null) ? this._namespaceName : this.ProviderManifest.NamespaceName);
			}
			string text4;
			if (text == null || !this.AliasResolver.TryResolveAlias(text3, out text4))
			{
				text4 = text3;
			}
			if (!this.SchemaManager.TryResolveType(text4, text2, out type))
			{
				if (text == null)
				{
					usingElement.AddError(ErrorCode.NotInNamespace, EdmSchemaErrorSeverity.Error, Strings.NotNamespaceQualified(typeName));
				}
				else if (!this.SchemaManager.IsValidNamespaceName(text4))
				{
					usingElement.AddError(ErrorCode.BadNamespace, EdmSchemaErrorSeverity.Error, Strings.BadNamespaceOrAlias(text));
				}
				else if (text4 != text3)
				{
					usingElement.AddError(ErrorCode.NotInNamespace, EdmSchemaErrorSeverity.Error, Strings.NotInNamespaceAlias(text2, text4, text3));
				}
				else
				{
					usingElement.AddError(ErrorCode.NotInNamespace, EdmSchemaErrorSeverity.Error, Strings.NotInNamespaceNoAlias(text2, text4));
				}
				return false;
			}
			if (this.DataModel != SchemaDataModelOption.EntityDataModel && type.Schema != this && type.Schema != this.SchemaManager.PrimitiveSchema)
			{
				usingElement.AddError(ErrorCode.InvalidNamespaceOrAliasSpecified, EdmSchemaErrorSeverity.Error, Strings.InvalidNamespaceOrAliasSpecified(text));
				return false;
			}
			return true;
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x0006A948 File Offset: 0x00068B48
		internal AliasResolver AliasResolver
		{
			get
			{
				if (this._aliasResolver == null)
				{
					this._aliasResolver = new AliasResolver(this);
				}
				return this._aliasResolver;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x0006A964 File Offset: 0x00068B64
		internal SchemaDataModelOption DataModel
		{
			get
			{
				return this.SchemaManager.DataModel;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x0006A971 File Offset: 0x00068B71
		internal SchemaManager SchemaManager
		{
			get
			{
				return this._schemaManager;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x0006A97C File Offset: 0x00068B7C
		internal bool UseStrongSpatialTypes
		{
			get
			{
				return this._useStrongSpatialTypes ?? true;
			}
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x0006A9A4 File Offset: 0x00068BA4
		private void HandleNamespaceAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this.Namespace);
			if (!returnValue.Succeeded)
			{
				return;
			}
			this.Namespace = returnValue.Value;
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x0006A9D4 File Offset: 0x00068BD4
		private void HandleAliasAttribute(XmlReader reader)
		{
			this.Alias = base.HandleUndottedNameAttribute(reader, this.Alias);
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x0006A9EC File Offset: 0x00068BEC
		private void HandleProviderAttribute(XmlReader reader)
		{
			string value = reader.Value;
			this._schemaManager.ProviderNotification(value, delegate(string message, ErrorCode code, EdmSchemaErrorSeverity severity)
			{
				this.AddError(code, severity, reader, message);
			});
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x0006AA38 File Offset: 0x00068C38
		private void HandleProviderManifestTokenAttribute(XmlReader reader)
		{
			string value = reader.Value;
			this._schemaManager.ProviderManifestTokenNotification(value, delegate(string message, ErrorCode code, EdmSchemaErrorSeverity severity)
			{
				this.AddError(code, severity, reader, message);
			});
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x0006AA84 File Offset: 0x00068C84
		private void HandleUseStrongSpatialTypesAnnotation(XmlReader reader)
		{
			bool flag = false;
			if (base.HandleBoolAttribute(reader, ref flag))
			{
				this._useStrongSpatialTypes = new bool?(flag);
			}
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x0006AAAC File Offset: 0x00068CAC
		private void HandleUsingElement(XmlReader reader)
		{
			UsingElement usingElement = new UsingElement(this);
			usingElement.Parse(reader);
			this.AliasResolver.Add(usingElement);
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x0006AAD4 File Offset: 0x00068CD4
		private void HandleEnumTypeElement(XmlReader reader)
		{
			SchemaEnumType schemaEnumType = new SchemaEnumType(this);
			schemaEnumType.Parse(reader);
			this.TryAddType(schemaEnumType, true);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x0006AAF8 File Offset: 0x00068CF8
		private void HandleTopLevelSchemaElement(XmlReader reader)
		{
			try
			{
				this._depth += 2;
				base.Parse(reader);
			}
			finally
			{
				this._depth -= 2;
			}
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x0006AB3C File Offset: 0x00068D3C
		private void HandleEntityTypeElement(XmlReader reader)
		{
			SchemaEntityType schemaEntityType = new SchemaEntityType(this);
			schemaEntityType.Parse(reader);
			this.TryAddType(schemaEntityType, true);
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x0006AB60 File Offset: 0x00068D60
		private void HandleTypeInformationElement(XmlReader reader)
		{
			TypeElement typeElement = new TypeElement(this);
			typeElement.Parse(reader);
			this.TryAddType(typeElement, true);
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x0006AB84 File Offset: 0x00068D84
		private void HandleFunctionElement(XmlReader reader)
		{
			Function function = new Function(this);
			function.Parse(reader);
			this.Functions.Add(function);
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x0006ABAC File Offset: 0x00068DAC
		private void HandleModelFunctionElement(XmlReader reader)
		{
			ModelFunction modelFunction = new ModelFunction(this);
			modelFunction.Parse(reader);
			this.Functions.Add(modelFunction);
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x0006ABD4 File Offset: 0x00068DD4
		private void HandleAssociationElement(XmlReader reader)
		{
			Relationship relationship = new Relationship(this, RelationshipKind.Association);
			relationship.Parse(reader);
			this.TryAddType(relationship, true);
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x0006ABF8 File Offset: 0x00068DF8
		private void HandleInlineTypeElement(XmlReader reader)
		{
			SchemaComplexType schemaComplexType = new SchemaComplexType(this);
			schemaComplexType.Parse(reader);
			this.TryAddType(schemaComplexType, true);
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x0006AC1C File Offset: 0x00068E1C
		private void HandleEntityContainerTypeElement(XmlReader reader)
		{
			EntityContainer entityContainer = new EntityContainer(this);
			entityContainer.Parse(reader);
			this.TryAddContainer(entityContainer, true);
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x0006AC3F File Offset: 0x00068E3F
		private List<EdmSchemaError> ResetErrors()
		{
			List<EdmSchemaError> errors = this._errors;
			this._errors = new List<EdmSchemaError>();
			return errors;
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x0006AC52 File Offset: 0x00068E52
		protected void TryAddType(SchemaType schemaType, bool doNotAddErrorForEmptyName)
		{
			this.SchemaManager.SchemaTypes.Add(schemaType, doNotAddErrorForEmptyName, new Func<object, string>(Strings.TypeNameAlreadyDefinedDuplicate));
			this.SchemaTypes.Add(schemaType);
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x0006AC7E File Offset: 0x00068E7E
		protected void TryAddContainer(SchemaType schemaType, bool doNotAddErrorForEmptyName)
		{
			this.SchemaManager.SchemaTypes.Add(schemaType, doNotAddErrorForEmptyName, new Func<object, string>(Strings.EntityContainerAlreadyExists));
			this.SchemaTypes.Add(schemaType);
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x0006ACAC File Offset: 0x00068EAC
		protected void AddFunctionType(Function function)
		{
			string text = ((this.DataModel == SchemaDataModelOption.EntityDataModel) ? "Conceptual" : "Storage");
			if (this.SchemaVersion >= 2.0 && this.SchemaManager.SchemaTypes.ContainsKey(function.FQName))
			{
				function.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.AmbiguousFunctionAndType(function.FQName, text));
				return;
			}
			if (this.SchemaManager.SchemaTypes.TryAdd(function) != AddErrorKind.Succeeded)
			{
				function.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.AmbiguousFunctionOverload(function.FQName, text));
				return;
			}
			this.SchemaTypes.Add(function);
		}

		// Token: 0x04000D1F RID: 3359
		private const int RootDepth = 2;

		// Token: 0x04000D20 RID: 3360
		private List<EdmSchemaError> _errors = new List<EdmSchemaError>();

		// Token: 0x04000D21 RID: 3361
		private List<Function> _functions;

		// Token: 0x04000D22 RID: 3362
		private AliasResolver _aliasResolver;

		// Token: 0x04000D23 RID: 3363
		private string _location;

		// Token: 0x04000D24 RID: 3364
		protected string _namespaceName;

		// Token: 0x04000D25 RID: 3365
		private List<SchemaType> _schemaTypes;

		// Token: 0x04000D26 RID: 3366
		private int _depth;

		// Token: 0x04000D27 RID: 3367
		private double _schemaVersion;

		// Token: 0x04000D28 RID: 3368
		private readonly SchemaManager _schemaManager;

		// Token: 0x04000D29 RID: 3369
		private bool? _useStrongSpatialTypes;

		// Token: 0x04000D2A RID: 3370
		private HashSet<string> _validatableXmlNamespaces;

		// Token: 0x04000D2B RID: 3371
		private HashSet<string> _parseableXmlNamespaces;

		// Token: 0x04000D2E RID: 3374
		private MetadataProperty _schemaSourceProperty;

		// Token: 0x020009B6 RID: 2486
		private static class SomSchemaSetHelper
		{
			// Token: 0x06005F2E RID: 24366 RVA: 0x00147600 File Offset: 0x00145800
			internal static List<string> GetPrimarySchemaNamespaces(SchemaDataModelOption dataModel)
			{
				List<string> list = new List<string>();
				if (dataModel == SchemaDataModelOption.EntityDataModel)
				{
					list.Add("http://schemas.microsoft.com/ado/2006/04/edm");
					list.Add("http://schemas.microsoft.com/ado/2007/05/edm");
					list.Add("http://schemas.microsoft.com/ado/2008/09/edm");
					list.Add("http://schemas.microsoft.com/ado/2009/11/edm");
				}
				else if (dataModel == SchemaDataModelOption.ProviderDataModel)
				{
					list.Add("http://schemas.microsoft.com/ado/2006/04/edm/ssdl");
					list.Add("http://schemas.microsoft.com/ado/2009/02/edm/ssdl");
					list.Add("http://schemas.microsoft.com/ado/2009/11/edm/ssdl");
				}
				else
				{
					list.Add("http://schemas.microsoft.com/ado/2006/04/edm/providermanifest");
				}
				return list;
			}

			// Token: 0x06005F2F RID: 24367 RVA: 0x00147677 File Offset: 0x00145877
			internal static XmlSchemaSet GetSchemaSet(SchemaDataModelOption dataModel)
			{
				return Schema.SomSchemaSetHelper._cachedSchemaSets.Evaluate(dataModel);
			}

			// Token: 0x06005F30 RID: 24368 RVA: 0x00147684 File Offset: 0x00145884
			private static XmlSchemaSet ComputeSchemaSet(SchemaDataModelOption dataModel)
			{
				List<string> primarySchemaNamespaces = Schema.SomSchemaSetHelper.GetPrimarySchemaNamespaces(dataModel);
				XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
				xmlSchemaSet.XmlResolver = null;
				Dictionary<string, XmlSchemaResource> metadataSchemaResourceMap = XmlSchemaResource.GetMetadataSchemaResourceMap(3.0);
				HashSet<string> hashSet = new HashSet<string>();
				foreach (string text in primarySchemaNamespaces)
				{
					XmlSchemaResource xmlSchemaResource = metadataSchemaResourceMap[text];
					Schema.SomSchemaSetHelper.AddXmlSchemaToSet(xmlSchemaSet, xmlSchemaResource, hashSet);
				}
				xmlSchemaSet.Compile();
				return xmlSchemaSet;
			}

			// Token: 0x06005F31 RID: 24369 RVA: 0x00147710 File Offset: 0x00145910
			private static void AddXmlSchemaToSet(XmlSchemaSet schemaSet, XmlSchemaResource schemaResource, HashSet<string> schemasAlreadyAdded)
			{
				foreach (XmlSchemaResource xmlSchemaResource in schemaResource.ImportedSchemas)
				{
					Schema.SomSchemaSetHelper.AddXmlSchemaToSet(schemaSet, xmlSchemaResource, schemasAlreadyAdded);
				}
				if (!schemasAlreadyAdded.Contains(schemaResource.NamespaceUri))
				{
					XmlSchema xmlSchema = XmlSchema.Read(Schema.SomSchemaSetHelper.GetResourceStream(schemaResource.ResourceName), null);
					schemaSet.Add(xmlSchema);
					schemasAlreadyAdded.Add(schemaResource.NamespaceUri);
				}
			}

			// Token: 0x06005F32 RID: 24370 RVA: 0x00147777 File Offset: 0x00145977
			private static Stream GetResourceStream(string resourceName)
			{
				return typeof(Schema).Assembly().GetManifestResourceStream(resourceName);
			}

			// Token: 0x040027F9 RID: 10233
			private static readonly Memoizer<SchemaDataModelOption, XmlSchemaSet> _cachedSchemaSets = new Memoizer<SchemaDataModelOption, XmlSchemaSet>(new Func<SchemaDataModelOption, XmlSchemaSet>(Schema.SomSchemaSetHelper.ComputeSchemaSet), EqualityComparer<SchemaDataModelOption>.Default);
		}
	}
}
