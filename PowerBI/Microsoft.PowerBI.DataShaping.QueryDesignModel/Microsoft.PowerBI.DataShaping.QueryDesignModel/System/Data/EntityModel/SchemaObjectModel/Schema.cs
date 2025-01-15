using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Xml;
using Microsoft.Data.Common;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000047 RID: 71
	[DebuggerDisplay("Namespace={Namespace}, PublicKeyToken={PublicKeyToken}, Version={Version}")]
	internal class Schema : SchemaElement
	{
		// Token: 0x060007B8 RID: 1976 RVA: 0x0000F8F3 File Offset: 0x0000DAF3
		public Schema(SchemaManager schemaManager)
			: base(null)
		{
			this._schemaManager = schemaManager;
			this._errors = new List<EdmSchemaError>();
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0000F919 File Offset: 0x0000DB19
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

		// Token: 0x060007BA RID: 1978 RVA: 0x0000F941 File Offset: 0x0000DB41
		internal IList<EdmSchemaError> ValidateSchema()
		{
			this.Validate();
			return this.ResetErrors();
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0000F94F File Offset: 0x0000DB4F
		internal void AddError(EdmSchemaError error)
		{
			this._errors.Add(error);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0000F960 File Offset: 0x0000DB60
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

		// Token: 0x060007BD RID: 1981 RVA: 0x0000F9B0 File Offset: 0x0000DBB0
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
					Func<object, object, object, string> func;
					if ((func = Schema.<>O.<0>__UnexpectedRootElement) == null)
					{
						func = (Schema.<>O.<0>__UnexpectedRootElement = new Func<object, object, object, string>(Strings.UnexpectedRootElement));
					}
					Func<object, object, object, string> func2 = func;
					if (string.IsNullOrEmpty(sourceReader.NamespaceURI))
					{
						Func<object, object, object, string> func3;
						if ((func3 = Schema.<>O.<1>__UnexpectedRootElementNoNamespace) == null)
						{
							func3 = (Schema.<>O.<1>__UnexpectedRootElementNoNamespace = new Func<object, object, object, string>(Strings.UnexpectedRootElementNoNamespace));
						}
						func2 = func3;
					}
					string commaDelimitedString = Helper.GetCommaDelimitedString(primarySchemaNamespaces);
					base.AddError(ErrorCode.UnexpectedXmlElement, EdmSchemaErrorSeverity.Error, func2(sourceReader.NamespaceURI, sourceReader.LocalName, commaDelimitedString));
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
						else
						{
							this.SchemaVersion = 2.0;
						}
					}
					else if (this.DataModel == SchemaDataModelOption.ProviderDataModel)
					{
						if (this.SchemaXmlNamespace == "http://schemas.microsoft.com/ado/2006/04/edm/ssdl")
						{
							this.SchemaVersion = 1.0;
						}
						else
						{
							this.SchemaVersion = 2.0;
						}
					}
					string localName = sourceReader.LocalName;
					if (localName == "Schema" || localName == "ProviderManifest")
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

		// Token: 0x060007BE RID: 1982 RVA: 0x0000FC74 File Offset: 0x0000DE74
		internal static XmlReaderSettings CreateEdmStandardXmlReaderSettings()
		{
			return new XmlReaderSettings
			{
				CheckCharacters = true,
				CloseInput = false,
				IgnoreWhitespace = true,
				ConformanceLevel = ConformanceLevel.Auto,
				IgnoreComments = true,
				IgnoreProcessingInstructions = true,
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0000FCB3 File Offset: 0x0000DEB3
		private XmlReaderSettings CreateXmlReaderSettings()
		{
			return Schema.CreateEdmStandardXmlReaderSettings();
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0000FCBC File Offset: 0x0000DEBC
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

		// Token: 0x060007C1 RID: 1985 RVA: 0x0000FD50 File Offset: 0x0000DF50
		public bool IsValidateableXmlNamespace(string xmlNamespaceUri, bool isAttribute)
		{
			if (string.IsNullOrEmpty(xmlNamespaceUri) && isAttribute)
			{
				return true;
			}
			if (this._validatableXmlNamespaces == null)
			{
				HashSet<string> hashSet = new HashSet<string>();
				foreach (XmlSchemaResource xmlSchemaResource in XmlSchemaResource.GetMetadataSchemaResourceMap((this.SchemaVersion == 0.0) ? 2.0 : this.SchemaVersion).Values)
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

		// Token: 0x060007C2 RID: 1986 RVA: 0x0000FE14 File Offset: 0x0000E014
		private static void AddAllSchemaResourceNamespaceNames(HashSet<string> hashSet, XmlSchemaResource schemaResource)
		{
			hashSet.Add(schemaResource.NamespaceUri);
			foreach (XmlSchemaResource xmlSchemaResource in schemaResource.ImportedSchemas)
			{
				Schema.AddAllSchemaResourceNamespaceNames(hashSet, xmlSchemaResource);
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0000FE54 File Offset: 0x0000E054
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

		// Token: 0x060007C4 RID: 1988 RVA: 0x0000FEF4 File Offset: 0x0000E0F4
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

		// Token: 0x060007C5 RID: 1989 RVA: 0x0000FF8C File Offset: 0x0000E18C
		internal override void Validate()
		{
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

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00010090 File Offset: 0x0000E290
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x00010098 File Offset: 0x0000E298
		internal string SchemaXmlNamespace
		{
			get
			{
				return this._schemaXmlNamespace;
			}
			private set
			{
				this._schemaXmlNamespace = value;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x000100A1 File Offset: 0x0000E2A1
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

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x000100BA File Offset: 0x0000E2BA
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x000100C2 File Offset: 0x0000E2C2
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

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x000100CB File Offset: 0x0000E2CB
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x000100D3 File Offset: 0x0000E2D3
		internal virtual string Alias
		{
			get
			{
				return this._alias;
			}
			private set
			{
				this._alias = value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x000100DC File Offset: 0x0000E2DC
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x000100E4 File Offset: 0x0000E2E4
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

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x000100ED File Offset: 0x0000E2ED
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x000100F5 File Offset: 0x0000E2F5
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

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00010100 File Offset: 0x0000E300
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

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0001014D File Offset: 0x0000E34D
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

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00010168 File Offset: 0x0000E368
		public override string FQName
		{
			get
			{
				return this.Namespace;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00010170 File Offset: 0x0000E370
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

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001018C File Offset: 0x0000E38C
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

		// Token: 0x060007D6 RID: 2006 RVA: 0x000102C4 File Offset: 0x0000E4C4
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

		// Token: 0x060007D7 RID: 2007 RVA: 0x000102E4 File Offset: 0x0000E4E4
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
			return false;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00010360 File Offset: 0x0000E560
		protected override void HandleAttributesComplete()
		{
			if (this._depth < 2)
			{
				return;
			}
			if (this._depth == 2)
			{
				this._schemaManager.EnsurePrimitiveSchemaIsLoaded();
			}
			base.HandleAttributesComplete();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00010388 File Offset: 0x0000E588
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

		// Token: 0x060007DA RID: 2010 RVA: 0x000103CC File Offset: 0x0000E5CC
		internal bool ResolveTypeName(SchemaElement usingElement, string typeName, out SchemaType type)
		{
			type = null;
			string text;
			string text2;
			Utils.ExtractNamespaceAndName(this.DataModel, typeName, out text, out text2);
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

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x000104CE File Offset: 0x0000E6CE
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

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000104EA File Offset: 0x0000E6EA
		internal SchemaDataModelOption DataModel
		{
			get
			{
				return this.SchemaManager.DataModel;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x000104F7 File Offset: 0x0000E6F7
		internal SchemaManager SchemaManager
		{
			get
			{
				return this._schemaManager;
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00010500 File Offset: 0x0000E700
		private void HandleNamespaceAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this.Namespace, null);
			if (!returnValue.Succeeded)
			{
				return;
			}
			this.Namespace = returnValue.Value;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00010531 File Offset: 0x0000E731
		private void HandleAliasAttribute(XmlReader reader)
		{
			this.Alias = base.HandleUndottedNameAttribute(reader, this.Alias);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00010548 File Offset: 0x0000E748
		private void HandleProviderAttribute(XmlReader reader)
		{
			string value = reader.Value;
			this._schemaManager.ProviderNotification(value, delegate(string message, ErrorCode code, EdmSchemaErrorSeverity severity)
			{
				this.AddError(code, severity, reader, message);
			});
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00010594 File Offset: 0x0000E794
		private void HandleProviderManifestTokenAttribute(XmlReader reader)
		{
			string value = reader.Value;
			this._schemaManager.ProviderManifestTokenNotification(value, delegate(string message, ErrorCode code, EdmSchemaErrorSeverity severity)
			{
				this.AddError(code, severity, reader, message);
			});
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000105E0 File Offset: 0x0000E7E0
		private void HandleUsingElement(XmlReader reader)
		{
			UsingElement usingElement = new UsingElement(this);
			usingElement.Parse(reader);
			this.AliasResolver.Add(usingElement);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00010608 File Offset: 0x0000E808
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

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001064C File Offset: 0x0000E84C
		private void HandleEntityTypeElement(XmlReader reader)
		{
			SchemaEntityType schemaEntityType = new SchemaEntityType(this);
			schemaEntityType.Parse(reader);
			this.TryAddType(schemaEntityType, true);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00010670 File Offset: 0x0000E870
		private void HandleTypeInformationElement(XmlReader reader)
		{
			TypeElement typeElement = new TypeElement(this);
			typeElement.Parse(reader);
			this.TryAddType(typeElement, true);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00010694 File Offset: 0x0000E894
		private void HandleFunctionElement(XmlReader reader)
		{
			Function function = new Function(this);
			function.Parse(reader);
			this.Functions.Add(function);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000106BC File Offset: 0x0000E8BC
		private void HandleModelFunctionElement(XmlReader reader)
		{
			ModelFunction modelFunction = new ModelFunction(this);
			modelFunction.Parse(reader);
			this.Functions.Add(modelFunction);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000106E4 File Offset: 0x0000E8E4
		private void HandleAssociationElement(XmlReader reader)
		{
			Relationship relationship = new Relationship(this, RelationshipKind.Association);
			relationship.Parse(reader);
			this.TryAddType(relationship, true);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00010708 File Offset: 0x0000E908
		private void HandleInlineTypeElement(XmlReader reader)
		{
			SchemaComplexType schemaComplexType = new SchemaComplexType(this);
			schemaComplexType.Parse(reader);
			this.TryAddType(schemaComplexType, true);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001072C File Offset: 0x0000E92C
		private void HandleEntityContainerTypeElement(XmlReader reader)
		{
			EntityContainer entityContainer = new EntityContainer(this);
			entityContainer.Parse(reader);
			this.TryAddContainer(entityContainer, true);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001074F File Offset: 0x0000E94F
		private List<EdmSchemaError> ResetErrors()
		{
			List<EdmSchemaError> errors = this._errors;
			this._errors = new List<EdmSchemaError>();
			return errors;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00010762 File Offset: 0x0000E962
		protected void TryAddType(SchemaType schemaType, bool doNotAddErrorForEmptyName)
		{
			SchemaElementLookUpTable<SchemaType> schemaTypes = this.SchemaManager.SchemaTypes;
			Func<object, string> func;
			if ((func = Schema.<>O.<2>__TypeNameAlreadyDefinedDuplicate) == null)
			{
				func = (Schema.<>O.<2>__TypeNameAlreadyDefinedDuplicate = new Func<object, string>(Strings.TypeNameAlreadyDefinedDuplicate));
			}
			schemaTypes.Add(schemaType, doNotAddErrorForEmptyName, func);
			this.SchemaTypes.Add(schemaType);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001079D File Offset: 0x0000E99D
		protected void TryAddContainer(SchemaType schemaType, bool doNotAddErrorForEmptyName)
		{
			SchemaElementLookUpTable<SchemaType> schemaTypes = this.SchemaManager.SchemaTypes;
			Func<object, string> func;
			if ((func = Schema.<>O.<3>__EntityContainerAlreadyExists) == null)
			{
				func = (Schema.<>O.<3>__EntityContainerAlreadyExists = new Func<object, string>(Strings.EntityContainerAlreadyExists));
			}
			schemaTypes.Add(schemaType, doNotAddErrorForEmptyName, func);
			this.SchemaTypes.Add(schemaType);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000107D8 File Offset: 0x0000E9D8
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

		// Token: 0x04000696 RID: 1686
		private const int RootDepth = 2;

		// Token: 0x04000697 RID: 1687
		private List<EdmSchemaError> _errors = new List<EdmSchemaError>();

		// Token: 0x04000698 RID: 1688
		private List<Function> _functions;

		// Token: 0x04000699 RID: 1689
		private AliasResolver _aliasResolver;

		// Token: 0x0400069A RID: 1690
		private string _location;

		// Token: 0x0400069B RID: 1691
		private string _alias;

		// Token: 0x0400069C RID: 1692
		protected string _namespaceName;

		// Token: 0x0400069D RID: 1693
		private string _schemaXmlNamespace;

		// Token: 0x0400069E RID: 1694
		private List<SchemaType> _schemaTypes;

		// Token: 0x0400069F RID: 1695
		private int _depth;

		// Token: 0x040006A0 RID: 1696
		private double _schemaVersion;

		// Token: 0x040006A1 RID: 1697
		private SchemaManager _schemaManager;

		// Token: 0x040006A2 RID: 1698
		private HashSet<string> _validatableXmlNamespaces;

		// Token: 0x040006A3 RID: 1699
		private HashSet<string> _parseableXmlNamespaces;

		// Token: 0x040006A4 RID: 1700
		private MetadataProperty _schemaSourceProperty;

		// Token: 0x0200029D RID: 669
		private static class SomSchemaSetHelper
		{
			// Token: 0x06001C1A RID: 7194 RVA: 0x0004E4F4 File Offset: 0x0004C6F4
			internal static List<string> GetPrimarySchemaNamespaces(SchemaDataModelOption dataModel)
			{
				List<string> list = new List<string>();
				if (dataModel == SchemaDataModelOption.EntityDataModel)
				{
					list.Add("http://schemas.microsoft.com/ado/2006/04/edm");
					list.Add("http://schemas.microsoft.com/ado/2007/05/edm");
					list.Add("http://schemas.microsoft.com/ado/2008/09/edm");
				}
				else if (dataModel == SchemaDataModelOption.ProviderDataModel)
				{
					list.Add("http://schemas.microsoft.com/ado/2006/04/edm/ssdl");
					list.Add("http://schemas.microsoft.com/ado/2009/02/edm/ssdl");
				}
				else
				{
					list.Add("http://schemas.microsoft.com/ado/2006/04/edm/providermanifest");
				}
				return list;
			}
		}

		// Token: 0x0200029E RID: 670
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000F6F RID: 3951
			public static Func<object, object, object, string> <0>__UnexpectedRootElement;

			// Token: 0x04000F70 RID: 3952
			public static Func<object, object, object, string> <1>__UnexpectedRootElementNoNamespace;

			// Token: 0x04000F71 RID: 3953
			public static Func<object, string> <2>__TypeNameAlreadyDefinedDuplicate;

			// Token: 0x04000F72 RID: 3954
			public static Func<object, string> <3>__EntityContainerAlreadyExists;
		}
	}
}
