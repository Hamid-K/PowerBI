using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AspNet.OData.Common
{
	// Token: 0x02000063 RID: 99
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class SRResources
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00002557 File Offset: 0x00000757
		internal SRResources()
		{
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000AFC0 File Offset: 0x000091C0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SRResources.resourceMan == null)
				{
					Assembly assembly = TypeHelper.GetAssembly(typeof(CommonWebApiResources));
					string text = (from s in assembly.GetManifestResourceNames()
						where s.EndsWith("SRResources.resources", StringComparison.OrdinalIgnoreCase)
						select s).Single<string>();
					text = text.Substring(0, text.Length - 10);
					SRResources.resourceMan = new ResourceManager(text, assembly);
				}
				return SRResources.resourceMan;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000B036 File Offset: 0x00009236
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000B03D File Offset: 0x0000923D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return SRResources.resourceCulture;
			}
			set
			{
				SRResources.resourceCulture = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000B045 File Offset: 0x00009245
		internal static string ActionContextMustHaveDescriptor
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionContextMustHaveDescriptor", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000B05B File Offset: 0x0000925B
		internal static string ActionContextMustHaveRequest
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionContextMustHaveRequest", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000B071 File Offset: 0x00009271
		internal static string ActionExecutedContextMustHaveActionContext
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionExecutedContextMustHaveActionContext", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000B087 File Offset: 0x00009287
		internal static string ActionExecutedContextMustHaveRequest
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionExecutedContextMustHaveRequest", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000B09D File Offset: 0x0000929D
		internal static string ActionNotBoundToCollectionOfEntity
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionNotBoundToCollectionOfEntity", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000B0B3 File Offset: 0x000092B3
		internal static string ActionNotBoundToEntity
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionNotBoundToEntity", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000B0C9 File Offset: 0x000092C9
		internal static string AggregateKindNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("AggregateKindNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000B0DF File Offset: 0x000092DF
		internal static string AggregationMethodNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("AggregationMethodNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000B0F5 File Offset: 0x000092F5
		internal static string AggregationNotSupportedForType
		{
			get
			{
				return SRResources.ResourceManager.GetString("AggregationNotSupportedForType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000B10B File Offset: 0x0000930B
		internal static string ApplyQueryOptionNotSupportedForLinq2SQL
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApplyQueryOptionNotSupportedForLinq2SQL", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000B121 File Offset: 0x00009321
		internal static string ApplyToOnUntypedQueryOption
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApplyToOnUntypedQueryOption", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000B137 File Offset: 0x00009337
		internal static string ArgumentMustBeOfType
		{
			get
			{
				return SRResources.ResourceManager.GetString("ArgumentMustBeOfType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000B14D File Offset: 0x0000934D
		internal static string BatchRequestInvalidMediaType
		{
			get
			{
				return SRResources.ResourceManager.GetString("BatchRequestInvalidMediaType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000B163 File Offset: 0x00009363
		internal static string BatchRequestMissingBoundary
		{
			get
			{
				return SRResources.ResourceManager.GetString("BatchRequestMissingBoundary", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000B179 File Offset: 0x00009379
		internal static string BatchRequestMissingContent
		{
			get
			{
				return SRResources.ResourceManager.GetString("BatchRequestMissingContent", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000B18F File Offset: 0x0000938F
		internal static string BatchRequestMissingContentType
		{
			get
			{
				return SRResources.ResourceManager.GetString("BatchRequestMissingContentType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000B1A5 File Offset: 0x000093A5
		internal static string BinaryOperatorNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("BinaryOperatorNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000B1BB File Offset: 0x000093BB
		internal static string CannotAddToNullCollection
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotAddToNullCollection", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000B1D1 File Offset: 0x000093D1
		internal static string CannotApplyETagOfT
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotApplyETagOfT", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000B1E7 File Offset: 0x000093E7
		internal static string CannotApplyODataQueryOptionsOfT
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotApplyODataQueryOptionsOfT", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000B1FD File Offset: 0x000093FD
		internal static string CannotAutoCreateMultipleCandidates
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotAutoCreateMultipleCandidates", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000B213 File Offset: 0x00009413
		internal static string CannotCastFilter
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotCastFilter", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000B229 File Offset: 0x00009429
		internal static string CannotDeserializeUnknownProperty
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotDeserializeUnknownProperty", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000B23F File Offset: 0x0000943F
		internal static string CannotDefineKeysOnDerivedTypes
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotDefineKeysOnDerivedTypes", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000B255 File Offset: 0x00009455
		internal static string CannotInferEdmType
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotInferEdmType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000B26B File Offset: 0x0000946B
		internal static string CannotInstantiateAbstractResourceType
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotInstantiateAbstractResourceType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B281 File Offset: 0x00009481
		internal static string CannotPatchNavigationProperties
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotPatchNavigationProperties", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000B297 File Offset: 0x00009497
		internal static string CannotRecognizeNodeType
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotRecognizeNodeType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B2AD File Offset: 0x000094AD
		internal static string CannotReconfigEntityTypeAsComplexType
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotReconfigEntityTypeAsComplexType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000B2C3 File Offset: 0x000094C3
		internal static string CannotRedefineBaseTypeProperty
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotRedefineBaseTypeProperty", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000B2D9 File Offset: 0x000094D9
		internal static string CannotReEnableDependencyInjection
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotReEnableDependencyInjection", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000B2EF File Offset: 0x000094EF
		internal static string CannotSerializerNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotSerializerNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000B305 File Offset: 0x00009505
		internal static string CannotSetDynamicPropertyDictionary
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotSetDynamicPropertyDictionary", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000B31B File Offset: 0x0000951B
		internal static string CannotWriteType
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotWriteType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000B331 File Offset: 0x00009531
		internal static string ClrTypeNotInModel
		{
			get
			{
				return SRResources.ResourceManager.GetString("ClrTypeNotInModel", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000B347 File Offset: 0x00009547
		internal static string CollectionParameterShouldHaveAddMethod
		{
			get
			{
				return SRResources.ResourceManager.GetString("CollectionParameterShouldHaveAddMethod", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000B35D File Offset: 0x0000955D
		internal static string CollectionPropertiesMustReturnIEnumerable
		{
			get
			{
				return SRResources.ResourceManager.GetString("CollectionPropertiesMustReturnIEnumerable", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000B373 File Offset: 0x00009573
		internal static string CollectionShouldHaveAddMethod
		{
			get
			{
				return SRResources.ResourceManager.GetString("CollectionShouldHaveAddMethod", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000B389 File Offset: 0x00009589
		internal static string CollectionShouldHaveClearMethod
		{
			get
			{
				return SRResources.ResourceManager.GetString("CollectionShouldHaveClearMethod", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000B39F File Offset: 0x0000959F
		internal static string ConvertToEnumFailed
		{
			get
			{
				return SRResources.ResourceManager.GetString("ConvertToEnumFailed", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B3B5 File Offset: 0x000095B5
		internal static string CreateODataValueNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("CreateODataValueNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000B3CB File Offset: 0x000095CB
		internal static string DeltaEntityTypeNotAssignable
		{
			get
			{
				return SRResources.ResourceManager.GetString("DeltaEntityTypeNotAssignable", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000B3E1 File Offset: 0x000095E1
		internal static string DeltaTypeMismatch
		{
			get
			{
				return SRResources.ResourceManager.GetString("DeltaTypeMismatch", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000B3F7 File Offset: 0x000095F7
		internal static string DeltaNestedResourceNameNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("DeltaNestedResourceNameNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B40D File Offset: 0x0000960D
		internal static string DependentAndPrincipalTypeNotMatch
		{
			get
			{
				return SRResources.ResourceManager.GetString("DependentAndPrincipalTypeNotMatch", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000B423 File Offset: 0x00009623
		internal static string DeserializerDoesNotSupportRead
		{
			get
			{
				return SRResources.ResourceManager.GetString("DeserializerDoesNotSupportRead", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000B439 File Offset: 0x00009639
		internal static string DoesNotSupportReadInLine
		{
			get
			{
				return SRResources.ResourceManager.GetString("DoesNotSupportReadInLine", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000B44F File Offset: 0x0000964F
		internal static string DuplicateDynamicPropertyNameFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("DuplicateDynamicPropertyNameFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B465 File Offset: 0x00009665
		internal static string DuplicateKeyInSegment
		{
			get
			{
				return SRResources.ResourceManager.GetString("DuplicateKeyInSegment", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000B47B File Offset: 0x0000967B
		internal static string DynamicPropertyCannotBeSerialized
		{
			get
			{
				return SRResources.ResourceManager.GetString("DynamicPropertyCannotBeSerialized", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000B491 File Offset: 0x00009691
		internal static string DynamicPropertyNameAlreadyUsedAsDeclaredPropertyName
		{
			get
			{
				return SRResources.ResourceManager.GetString("DynamicPropertyNameAlreadyUsedAsDeclaredPropertyName", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000B4A7 File Offset: 0x000096A7
		internal static string DynamicResourceSetTypeNameIsRequired
		{
			get
			{
				return SRResources.ResourceManager.GetString("DynamicResourceSetTypeNameIsRequired", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000B4BD File Offset: 0x000096BD
		internal static string EditLinkNullForLocationHeader
		{
			get
			{
				return SRResources.ResourceManager.GetString("EditLinkNullForLocationHeader", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000B4D3 File Offset: 0x000096D3
		internal static string EdmComplexObjectNullRef
		{
			get
			{
				return SRResources.ResourceManager.GetString("EdmComplexObjectNullRef", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000B4E9 File Offset: 0x000096E9
		internal static string EdmObjectNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("EdmObjectNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000B4FF File Offset: 0x000096FF
		internal static string EdmTypeCannotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("EdmTypeCannotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000B515 File Offset: 0x00009715
		internal static string EdmTypeNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("EdmTypeNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000B52B File Offset: 0x0000972B
		internal static string ElementClrTypeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("ElementClrTypeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000B541 File Offset: 0x00009741
		internal static string EmptyKeyTemplate
		{
			get
			{
				return SRResources.ResourceManager.GetString("EmptyKeyTemplate", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000B557 File Offset: 0x00009757
		internal static string EmptyParameterAlias
		{
			get
			{
				return SRResources.ResourceManager.GetString("EmptyParameterAlias", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000B56D File Offset: 0x0000976D
		internal static string EntityReferenceMustHasKeySegment
		{
			get
			{
				return SRResources.ResourceManager.GetString("EntityReferenceMustHasKeySegment", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000B583 File Offset: 0x00009783
		internal static string EntitySetAlreadyConfiguredDifferentEntityType
		{
			get
			{
				return SRResources.ResourceManager.GetString("EntitySetAlreadyConfiguredDifferentEntityType", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000B599 File Offset: 0x00009799
		internal static string EntitySetMissingDuringSerialization
		{
			get
			{
				return SRResources.ResourceManager.GetString("EntitySetMissingDuringSerialization", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000B5AF File Offset: 0x000097AF
		internal static string EntitySetNameAlreadyConfiguredAsSingleton
		{
			get
			{
				return SRResources.ResourceManager.GetString("EntitySetNameAlreadyConfiguredAsSingleton", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000B5C5 File Offset: 0x000097C5
		internal static string EntitySetNotFoundForName
		{
			get
			{
				return SRResources.ResourceManager.GetString("EntitySetNotFoundForName", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000B5DB File Offset: 0x000097DB
		internal static string EntityTypeDoesntHaveKeyDefined
		{
			get
			{
				return SRResources.ResourceManager.GetString("EntityTypeDoesntHaveKeyDefined", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000B5F1 File Offset: 0x000097F1
		internal static string CollectionNavigationPropertyEntityTypeDoesntHaveKeyDefined
		{
			get
			{
				return SRResources.ResourceManager.GetString("CollectionNavigationPropertyEntityTypeDoesntHaveKeyDefined", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000B607 File Offset: 0x00009807
		internal static string EntityTypeMismatch
		{
			get
			{
				return SRResources.ResourceManager.GetString("EntityTypeMismatch", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000B61D File Offset: 0x0000981D
		internal static string EnumTypeDoesNotExist
		{
			get
			{
				return SRResources.ResourceManager.GetString("EnumTypeDoesNotExist", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000B633 File Offset: 0x00009833
		internal static string EnumValueCannotBeLong
		{
			get
			{
				return SRResources.ResourceManager.GetString("EnumValueCannotBeLong", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000B649 File Offset: 0x00009849
		internal static string EqualExpressionsMustHaveSameTypes
		{
			get
			{
				return SRResources.ResourceManager.GetString("EqualExpressionsMustHaveSameTypes", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000B65F File Offset: 0x0000985F
		internal static string ErrorTypeMustBeODataErrorOrHttpError
		{
			get
			{
				return SRResources.ResourceManager.GetString("ErrorTypeMustBeODataErrorOrHttpError", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000B675 File Offset: 0x00009875
		internal static string ETagNotWellFormed
		{
			get
			{
				return SRResources.ResourceManager.GetString("ETagNotWellFormed", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000B68B File Offset: 0x0000988B
		internal static string ExpandFilterExpressionNotLambdaExpression
		{
			get
			{
				return SRResources.ResourceManager.GetString("ExpandFilterExpressionNotLambdaExpression", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000B6A1 File Offset: 0x000098A1
		internal static string FailedToBuildEdmModelBecauseReturnTypeIsNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("FailedToBuildEdmModelBecauseReturnTypeIsNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000B6B7 File Offset: 0x000098B7
		internal static string FailedToRetrieveTypeToBuildEdmModel
		{
			get
			{
				return SRResources.ResourceManager.GetString("FailedToRetrieveTypeToBuildEdmModel", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000B6CD File Offset: 0x000098CD
		internal static string FormatterReadIsNotSupportedForType
		{
			get
			{
				return SRResources.ResourceManager.GetString("FormatterReadIsNotSupportedForType", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000B6E3 File Offset: 0x000098E3
		internal static string FunctionNotBoundToCollectionOfEntity
		{
			get
			{
				return SRResources.ResourceManager.GetString("FunctionNotBoundToCollectionOfEntity", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000B6F9 File Offset: 0x000098F9
		internal static string FunctionNotBoundToEntity
		{
			get
			{
				return SRResources.ResourceManager.GetString("FunctionNotBoundToEntity", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000B70F File Offset: 0x0000990F
		internal static string FunctionNotSupportedOnEnum
		{
			get
			{
				return SRResources.ResourceManager.GetString("FunctionNotSupportedOnEnum", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000B725 File Offset: 0x00009925
		internal static string FunctionParameterNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("FunctionParameterNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000B73B File Offset: 0x0000993B
		internal static string GetEdmModelCalledMoreThanOnce
		{
			get
			{
				return SRResources.ResourceManager.GetString("GetEdmModelCalledMoreThanOnce", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000B751 File Offset: 0x00009951
		internal static string GetOnlyCollectionCannotBeArray
		{
			get
			{
				return SRResources.ResourceManager.GetString("GetOnlyCollectionCannotBeArray", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000B767 File Offset: 0x00009967
		internal static string HasActionLinkRequiresBindToCollectionOfEntity
		{
			get
			{
				return SRResources.ResourceManager.GetString("HasActionLinkRequiresBindToCollectionOfEntity", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000B77D File Offset: 0x0000997D
		internal static string HasActionLinkRequiresBindToEntity
		{
			get
			{
				return SRResources.ResourceManager.GetString("HasActionLinkRequiresBindToEntity", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000B793 File Offset: 0x00009993
		internal static string HasFunctionLinkRequiresBindToCollectionOfEntity
		{
			get
			{
				return SRResources.ResourceManager.GetString("HasFunctionLinkRequiresBindToCollectionOfEntity", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000B7A9 File Offset: 0x000099A9
		internal static string HasFunctionLinkRequiresBindToEntity
		{
			get
			{
				return SRResources.ResourceManager.GetString("HasFunctionLinkRequiresBindToEntity", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000B7BF File Offset: 0x000099BF
		internal static string IdLinkNullForEntityIdHeader
		{
			get
			{
				return SRResources.ResourceManager.GetString("IdLinkNullForEntityIdHeader", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000B7D5 File Offset: 0x000099D5
		internal static string InvalidAttributeRoutingTemplateSegment
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidAttributeRoutingTemplateSegment", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000B7EB File Offset: 0x000099EB
		internal static string InvalidBatchReaderState
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidBatchReaderState", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000B801 File Offset: 0x00009A01
		internal static string InvalidBindingParameterType
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidBindingParameterType", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000B817 File Offset: 0x00009A17
		internal static string InvalidDollarId
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidDollarId", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000B82D File Offset: 0x00009A2D
		internal static string InvalidEntitySetName
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidEntitySetName", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000B843 File Offset: 0x00009A43
		internal static string InvalidETagHandler
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidETagHandler", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000B859 File Offset: 0x00009A59
		internal static string InvalidExpansionDepthValue
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidExpansionDepthValue", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000B86F File Offset: 0x00009A6F
		internal static string InvalidODataPathTemplate
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidODataPathTemplate", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000B885 File Offset: 0x00009A85
		internal static string InvalidODataRouteOnAction
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidODataRouteOnAction", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000B89B File Offset: 0x00009A9B
		internal static string InvalidODataUntypedValue
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidODataUntypedValue", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000B8B1 File Offset: 0x00009AB1
		internal static string InvalidPathSegment
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidPathSegment", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000B8C7 File Offset: 0x00009AC7
		internal static string InvalidPropertyInfoForDynamicPropertyAnnotation
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidPropertyInfoForDynamicPropertyAnnotation", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000B8DD File Offset: 0x00009ADD
		internal static string InvalidPropertyMapper
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidPropertyMapper", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000B8F3 File Offset: 0x00009AF3
		internal static string InvalidPropertyMapping
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidPropertyMapping", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000B909 File Offset: 0x00009B09
		internal static string InvalidSingleQuoteCountForNonStringLiteral
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidSingleQuoteCountForNonStringLiteral", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000B91F File Offset: 0x00009B1F
		internal static string InvalidSingletonName
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidSingletonName", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000B935 File Offset: 0x00009B35
		internal static string InvalidTimeZoneInfo
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidTimeZoneInfo", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000B94B File Offset: 0x00009B4B
		internal static string KeyTemplateMustBeInCurlyBraces
		{
			get
			{
				return SRResources.ResourceManager.GetString("KeyTemplateMustBeInCurlyBraces", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000B961 File Offset: 0x00009B61
		internal static string KeyValueCannotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("KeyValueCannotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000B977 File Offset: 0x00009B77
		internal static string LambdaExpressionMustHaveExactlyOneParameter
		{
			get
			{
				return SRResources.ResourceManager.GetString("LambdaExpressionMustHaveExactlyOneParameter", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000B98D File Offset: 0x00009B8D
		internal static string LambdaExpressionMustHaveExactlyTwoParameters
		{
			get
			{
				return SRResources.ResourceManager.GetString("LambdaExpressionMustHaveExactlyTwoParameters", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B9A3 File Offset: 0x00009BA3
		internal static string LiteralHasABadFormat
		{
			get
			{
				return SRResources.ResourceManager.GetString("LiteralHasABadFormat", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000B9B9 File Offset: 0x00009BB9
		internal static string ManyNavigationPropertiesCannotBeChanged
		{
			get
			{
				return SRResources.ResourceManager.GetString("ManyNavigationPropertiesCannotBeChanged", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000B9CF File Offset: 0x00009BCF
		internal static string ManyToManyNavigationPropertyMustReturnCollection
		{
			get
			{
				return SRResources.ResourceManager.GetString("ManyToManyNavigationPropertyMustReturnCollection", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000B9E5 File Offset: 0x00009BE5
		internal static string MappingDoesNotContainResourceType
		{
			get
			{
				return SRResources.ResourceManager.GetString("MappingDoesNotContainResourceType", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000B9FB File Offset: 0x00009BFB
		internal static string MaxAnyAllExpressionLimitExceeded
		{
			get
			{
				return SRResources.ResourceManager.GetString("MaxAnyAllExpressionLimitExceeded", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000BA11 File Offset: 0x00009C11
		internal static string MaxExpandDepthExceeded
		{
			get
			{
				return SRResources.ResourceManager.GetString("MaxExpandDepthExceeded", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000BA27 File Offset: 0x00009C27
		internal static string MaxNodeLimitExceeded
		{
			get
			{
				return SRResources.ResourceManager.GetString("MaxNodeLimitExceeded", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000BA3D File Offset: 0x00009C3D
		internal static string MemberExpressionsMustBeBoundToLambdaParameter
		{
			get
			{
				return SRResources.ResourceManager.GetString("MemberExpressionsMustBeBoundToLambdaParameter", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000BA53 File Offset: 0x00009C53
		internal static string MemberExpressionsMustBeProperties
		{
			get
			{
				return SRResources.ResourceManager.GetString("MemberExpressionsMustBeProperties", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000BA69 File Offset: 0x00009C69
		internal static string MissingODataServices
		{
			get
			{
				return SRResources.ResourceManager.GetString("MissingODataServices", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000BA7F File Offset: 0x00009C7F
		internal static string MissingODataContainer
		{
			get
			{
				return SRResources.ResourceManager.GetString("MissingODataContainer", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000BA95 File Offset: 0x00009C95
		internal static string MissingNonODataContainer
		{
			get
			{
				return SRResources.ResourceManager.GetString("MissingNonODataContainer", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000BAAB File Offset: 0x00009CAB
		internal static string ModelBinderUtil_ModelMetadataCannotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderUtil_ModelMetadataCannotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000BAC1 File Offset: 0x00009CC1
		internal static string ModelBinderUtil_ValueCannotBeEnum
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderUtil_ValueCannotBeEnum", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000BAD7 File Offset: 0x00009CD7
		internal static string ModelMissingFromReadContext
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelMissingFromReadContext", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000BAED File Offset: 0x00009CED
		internal static string MoreThanOneDynamicPropertyContainerFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("MoreThanOneDynamicPropertyContainerFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000BB03 File Offset: 0x00009D03
		internal static string MoreThanOneOperationFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("MoreThanOneOperationFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000BB19 File Offset: 0x00009D19
		internal static string MoreThanOneOverloadActionBoundToSameTypeFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("MoreThanOneOverloadActionBoundToSameTypeFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000BB2F File Offset: 0x00009D2F
		internal static string MoreThanOneUnboundActionFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("MoreThanOneUnboundActionFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000BB45 File Offset: 0x00009D45
		internal static string MultipleAttributesFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("MultipleAttributesFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000BB5B File Offset: 0x00009D5B
		internal static string MultipleMatchingClrTypesForEdmType
		{
			get
			{
				return SRResources.ResourceManager.GetString("MultipleMatchingClrTypesForEdmType", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000BB71 File Offset: 0x00009D71
		internal static string MustBeCollectionProperty
		{
			get
			{
				return SRResources.ResourceManager.GetString("MustBeCollectionProperty", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000BB87 File Offset: 0x00009D87
		internal static string MustBeComplexProperty
		{
			get
			{
				return SRResources.ResourceManager.GetString("MustBeComplexProperty", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000BB9D File Offset: 0x00009D9D
		internal static string MustBeDateTimeProperty
		{
			get
			{
				return SRResources.ResourceManager.GetString("MustBeDateTimeProperty", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000BBB3 File Offset: 0x00009DB3
		internal static string MustBeEnumProperty
		{
			get
			{
				return SRResources.ResourceManager.GetString("MustBeEnumProperty", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000BBC9 File Offset: 0x00009DC9
		internal static string MustBeNavigationProperty
		{
			get
			{
				return SRResources.ResourceManager.GetString("MustBeNavigationProperty", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000BBDF File Offset: 0x00009DDF
		internal static string MustBePrimitiveProperty
		{
			get
			{
				return SRResources.ResourceManager.GetString("MustBePrimitiveProperty", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000BBF5 File Offset: 0x00009DF5
		internal static string MustBePrimitiveType
		{
			get
			{
				return SRResources.ResourceManager.GetString("MustBePrimitiveType", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000BC0B File Offset: 0x00009E0B
		internal static string MustBeTimeSpanProperty
		{
			get
			{
				return SRResources.ResourceManager.GetString("MustBeTimeSpanProperty", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000BC21 File Offset: 0x00009E21
		internal static string MustHaveMatchingMultiplicity
		{
			get
			{
				return SRResources.ResourceManager.GetString("MustHaveMatchingMultiplicity", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000BC37 File Offset: 0x00009E37
		internal static string NavigationPropertyBindingPathIsNotValid
		{
			get
			{
				return SRResources.ResourceManager.GetString("NavigationPropertyBindingPathIsNotValid", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000BC4D File Offset: 0x00009E4D
		internal static string NavigationPropertyBindingPathNotInHierarchy
		{
			get
			{
				return SRResources.ResourceManager.GetString("NavigationPropertyBindingPathNotInHierarchy", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000BC63 File Offset: 0x00009E63
		internal static string NavigationPropertyBindingPathNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("NavigationPropertyBindingPathNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000BC79 File Offset: 0x00009E79
		internal static string NavigationPropertyNotInHierarchy
		{
			get
			{
				return SRResources.ResourceManager.GetString("NavigationPropertyNotInHierarchy", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000BC8F File Offset: 0x00009E8F
		internal static string NavigationSourceMissingDuringDeserialization
		{
			get
			{
				return SRResources.ResourceManager.GetString("NavigationSourceMissingDuringDeserialization", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000BCA5 File Offset: 0x00009EA5
		internal static string NavigationSourceMissingDuringSerialization
		{
			get
			{
				return SRResources.ResourceManager.GetString("NavigationSourceMissingDuringSerialization", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000BCBB File Offset: 0x00009EBB
		internal static string NavigationSourceTypeHasNoKeys
		{
			get
			{
				return SRResources.ResourceManager.GetString("NavigationSourceTypeHasNoKeys", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000BCD1 File Offset: 0x00009ED1
		internal static string EntitySetTypeHasNoKeys
		{
			get
			{
				return SRResources.ResourceManager.GetString("EntitySetTypeHasNoKeys", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000BCE7 File Offset: 0x00009EE7
		internal static string NestedCollectionsNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("NestedCollectionsNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000BCFD File Offset: 0x00009EFD
		internal static string NestedPropertyNotfound
		{
			get
			{
				return SRResources.ResourceManager.GetString("NestedPropertyNotfound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000BD13 File Offset: 0x00009F13
		internal static string NoKeyNameFoundInSegment
		{
			get
			{
				return SRResources.ResourceManager.GetString("NoKeyNameFoundInSegment", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000BD29 File Offset: 0x00009F29
		internal static string NoMatchingIEdmTypeFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("NoMatchingIEdmTypeFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000BD3F File Offset: 0x00009F3F
		internal static string NoMatchingResource
		{
			get
			{
				return SRResources.ResourceManager.GetString("NoMatchingResource", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000BD55 File Offset: 0x00009F55
		internal static string NonNullUriRequiredForMediaTypeMapping
		{
			get
			{
				return SRResources.ResourceManager.GetString("NonNullUriRequiredForMediaTypeMapping", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000BD6B File Offset: 0x00009F6B
		internal static string NoNonODataHttpRouteRegistered
		{
			get
			{
				return SRResources.ResourceManager.GetString("NoNonODataHttpRouteRegistered", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000BD81 File Offset: 0x00009F81
		internal static string NonSelectExpandOnSingleEntity
		{
			get
			{
				return SRResources.ResourceManager.GetString("NonSelectExpandOnSingleEntity", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000BD97 File Offset: 0x00009F97
		internal static string NoRoutingHandlerToSelectAction
		{
			get
			{
				return SRResources.ResourceManager.GetString("NoRoutingHandlerToSelectAction", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000BDAD File Offset: 0x00009FAD
		internal static string NotAllowedArithmeticOperator
		{
			get
			{
				return SRResources.ResourceManager.GetString("NotAllowedArithmeticOperator", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000BDC3 File Offset: 0x00009FC3
		internal static string NotAllowedFunction
		{
			get
			{
				return SRResources.ResourceManager.GetString("NotAllowedFunction", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000BDD9 File Offset: 0x00009FD9
		internal static string NotAllowedLogicalOperator
		{
			get
			{
				return SRResources.ResourceManager.GetString("NotAllowedLogicalOperator", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000BDEF File Offset: 0x00009FEF
		internal static string NotAllowedOrderByProperty
		{
			get
			{
				return SRResources.ResourceManager.GetString("NotAllowedOrderByProperty", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000BE05 File Offset: 0x0000A005
		internal static string NotAllowedQueryOption
		{
			get
			{
				return SRResources.ResourceManager.GetString("NotAllowedQueryOption", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000BE1B File Offset: 0x0000A01B
		internal static string NotCountableEntitySetUsedForCount
		{
			get
			{
				return SRResources.ResourceManager.GetString("NotCountableEntitySetUsedForCount", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000BE31 File Offset: 0x0000A031
		internal static string NotCountablePropertyUsedForCount
		{
			get
			{
				return SRResources.ResourceManager.GetString("NotCountablePropertyUsedForCount", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000BE47 File Offset: 0x0000A047
		internal static string NotExpandablePropertyUsedInExpand
		{
			get
			{
				return SRResources.ResourceManager.GetString("NotExpandablePropertyUsedInExpand", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000BE5D File Offset: 0x0000A05D
		internal static string NotFilterablePropertyUsedInFilter
		{
			get
			{
				return SRResources.ResourceManager.GetString("NotFilterablePropertyUsedInFilter", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000BE73 File Offset: 0x0000A073
		internal static string NotNavigablePropertyUsedInNavigation
		{
			get
			{
				return SRResources.ResourceManager.GetString("NotNavigablePropertyUsedInNavigation", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000BE89 File Offset: 0x0000A089
		internal static string NotSelectablePropertyUsedInSelect
		{
			get
			{
				return SRResources.ResourceManager.GetString("NotSelectablePropertyUsedInSelect", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000BE9F File Offset: 0x0000A09F
		internal static string NotSortablePropertyUsedInOrderBy
		{
			get
			{
				return SRResources.ResourceManager.GetString("NotSortablePropertyUsedInOrderBy", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000BEB5 File Offset: 0x0000A0B5
		internal static string NotSupportedTransformationKind
		{
			get
			{
				return SRResources.ResourceManager.GetString("NotSupportedTransformationKind", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000BECB File Offset: 0x0000A0CB
		internal static string NoValueLiteralFoundInSegment
		{
			get
			{
				return SRResources.ResourceManager.GetString("NoValueLiteralFoundInSegment", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000BEE1 File Offset: 0x0000A0E1
		internal static string NullContainer
		{
			get
			{
				return SRResources.ResourceManager.GetString("NullContainer", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000BEF7 File Offset: 0x0000A0F7
		internal static string NullContainerBuilder
		{
			get
			{
				return SRResources.ResourceManager.GetString("NullContainerBuilder", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000BF0D File Offset: 0x0000A10D
		internal static string NullElementInCollection
		{
			get
			{
				return SRResources.ResourceManager.GetString("NullElementInCollection", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000BF23 File Offset: 0x0000A123
		internal static string NullETagHandler
		{
			get
			{
				return SRResources.ResourceManager.GetString("NullETagHandler", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000BF39 File Offset: 0x0000A139
		internal static string NullOnNonNullableFunctionParameter
		{
			get
			{
				return SRResources.ResourceManager.GetString("NullOnNonNullableFunctionParameter", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000BF4F File Offset: 0x0000A14F
		internal static string Object_NotYetInitialized
		{
			get
			{
				return SRResources.ResourceManager.GetString("Object_NotYetInitialized", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000BF65 File Offset: 0x0000A165
		internal static string ODataFunctionNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("ODataFunctionNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000BF7B File Offset: 0x0000A17B
		internal static string ODataPathMissing
		{
			get
			{
				return SRResources.ResourceManager.GetString("ODataPathMissing", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000BF91 File Offset: 0x0000A191
		internal static string ODataPathNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("ODataPathNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000BFA7 File Offset: 0x0000A1A7
		internal static string OperationHasInvalidEntitySetPath
		{
			get
			{
				return SRResources.ResourceManager.GetString("OperationHasInvalidEntitySetPath", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000BFBD File Offset: 0x0000A1BD
		internal static string OperationImportSegmentMustBeFunction
		{
			get
			{
				return SRResources.ResourceManager.GetString("OperationImportSegmentMustBeFunction", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000BFD3 File Offset: 0x0000A1D3
		internal static string OperationSegmentMustBeFunction
		{
			get
			{
				return SRResources.ResourceManager.GetString("OperationSegmentMustBeFunction", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000BFE9 File Offset: 0x0000A1E9
		internal static string OrderByClauseNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("OrderByClauseNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000BFFF File Offset: 0x0000A1FF
		internal static string OrderByDuplicateIt
		{
			get
			{
				return SRResources.ResourceManager.GetString("OrderByDuplicateIt", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000C015 File Offset: 0x0000A215
		internal static string OrderByDuplicateProperty
		{
			get
			{
				return SRResources.ResourceManager.GetString("OrderByDuplicateProperty", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000C02B File Offset: 0x0000A22B
		internal static string OrderByNodeCountExceeded
		{
			get
			{
				return SRResources.ResourceManager.GetString("OrderByNodeCountExceeded", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000C041 File Offset: 0x0000A241
		internal static string ParameterAliasMustBeInCurlyBraces
		{
			get
			{
				return SRResources.ResourceManager.GetString("ParameterAliasMustBeInCurlyBraces", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000C057 File Offset: 0x0000A257
		internal static string ParameterTypeIsNotCollection
		{
			get
			{
				return SRResources.ResourceManager.GetString("ParameterTypeIsNotCollection", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000C06D File Offset: 0x0000A26D
		internal static string PropertyAlreadyDefinedInDerivedType
		{
			get
			{
				return SRResources.ResourceManager.GetString("PropertyAlreadyDefinedInDerivedType", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000C083 File Offset: 0x0000A283
		internal static string PropertyDoesNotBelongToType
		{
			get
			{
				return SRResources.ResourceManager.GetString("PropertyDoesNotBelongToType", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000C099 File Offset: 0x0000A299
		internal static string PropertyIsNotCollection
		{
			get
			{
				return SRResources.ResourceManager.GetString("PropertyIsNotCollection", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000C0AF File Offset: 0x0000A2AF
		internal static string PropertyMustBeDateTimeOffsetOrDate
		{
			get
			{
				return SRResources.ResourceManager.GetString("PropertyMustBeDateTimeOffsetOrDate", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000C0C5 File Offset: 0x0000A2C5
		internal static string PropertyMustBeBoolean
		{
			get
			{
				return SRResources.ResourceManager.GetString("PropertyMustBeBoolean", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000C0DB File Offset: 0x0000A2DB
		internal static string PropertyMustBeEnum
		{
			get
			{
				return SRResources.ResourceManager.GetString("PropertyMustBeEnum", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000C0F1 File Offset: 0x0000A2F1
		internal static string PropertyMustBeString
		{
			get
			{
				return SRResources.ResourceManager.GetString("PropertyMustBeString", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000C107 File Offset: 0x0000A307
		internal static string PropertyMustBeStringLengthOne
		{
			get
			{
				return SRResources.ResourceManager.GetString("PropertyMustBeStringLengthOne", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000C11D File Offset: 0x0000A31D
		internal static string PropertyMustBeStringMaxLengthOne
		{
			get
			{
				return SRResources.ResourceManager.GetString("PropertyMustBeStringMaxLengthOne", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000C133 File Offset: 0x0000A333
		internal static string PropertyMustBeTimeOfDay
		{
			get
			{
				return SRResources.ResourceManager.GetString("PropertyMustBeTimeOfDay", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000C149 File Offset: 0x0000A349
		internal static string PropertyMustHavePublicGetterAndSetter
		{
			get
			{
				return SRResources.ResourceManager.GetString("PropertyMustHavePublicGetterAndSetter", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000C15F File Offset: 0x0000A35F
		internal static string PropertyNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("PropertyNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000C175 File Offset: 0x0000A375
		internal static string PropertyOrPathWasRemovedFromContext
		{
			get
			{
				return SRResources.ResourceManager.GetString("PropertyOrPathWasRemovedFromContext", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000C18B File Offset: 0x0000A38B
		internal static string QueryCannotBeEmpty
		{
			get
			{
				return SRResources.ResourceManager.GetString("QueryCannotBeEmpty", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000C1A1 File Offset: 0x0000A3A1
		internal static string QueryGetModelMustNotReturnNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("QueryGetModelMustNotReturnNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000C1B7 File Offset: 0x0000A3B7
		internal static string QueryingRequiresObjectContent
		{
			get
			{
				return SRResources.ResourceManager.GetString("QueryingRequiresObjectContent", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000C1CD File Offset: 0x0000A3CD
		internal static string QueryNodeBindingNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("QueryNodeBindingNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000C1E3 File Offset: 0x0000A3E3
		internal static string QueryNodeValidationNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("QueryNodeValidationNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000C1F9 File Offset: 0x0000A3F9
		internal static string QueryParameterNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("QueryParameterNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000C20F File Offset: 0x0000A40F
		internal static string ReadFromStreamAsyncMustHaveRequest
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReadFromStreamAsyncMustHaveRequest", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000C225 File Offset: 0x0000A425
		internal static string RebindingNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("RebindingNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000C23B File Offset: 0x0000A43B
		internal static string ReferenceNavigationPropertyExpandFilterVisitorUnexpectedParameter
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReferenceNavigationPropertyExpandFilterVisitorUnexpectedParameter", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000C251 File Offset: 0x0000A451
		internal static string ReferentialConstraintAlreadyConfigured
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReferentialConstraintAlreadyConfigured", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000C267 File Offset: 0x0000A467
		internal static string ReferentialConstraintOnManyNavigationPropertyNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReferentialConstraintOnManyNavigationPropertyNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000C27D File Offset: 0x0000A47D
		internal static string ReferentialConstraintPropertyTypeNotValid
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReferentialConstraintPropertyTypeNotValid", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000C293 File Offset: 0x0000A493
		internal static string RequestContainerAlreadyExists
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestContainerAlreadyExists", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000C2A9 File Offset: 0x0000A4A9
		internal static string RequestMustContainConfiguration
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestMustContainConfiguration", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000C2BF File Offset: 0x0000A4BF
		internal static string RequestMustHaveModel
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestMustHaveModel", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000C2D5 File Offset: 0x0000A4D5
		internal static string RequestMustHaveODataRouteName
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestMustHaveODataRouteName", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000C2EB File Offset: 0x0000A4EB
		internal static string RequestNotActionInvocation
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestNotActionInvocation", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000C301 File Offset: 0x0000A501
		internal static string RequestUriTooShortForODataPath
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestUriTooShortForODataPath", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000C317 File Offset: 0x0000A517
		internal static string ResourceTypeNotInModel
		{
			get
			{
				return SRResources.ResourceManager.GetString("ResourceTypeNotInModel", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000C32D File Offset: 0x0000A52D
		internal static string ReturnEntityCollectionWithoutEntitySet
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReturnEntityCollectionWithoutEntitySet", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000C343 File Offset: 0x0000A543
		internal static string ReturnEntityWithoutEntitySet
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReturnEntityWithoutEntitySet", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000C359 File Offset: 0x0000A559
		internal static string RootElementNameMissing
		{
			get
			{
				return SRResources.ResourceManager.GetString("RootElementNameMissing", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000C36F File Offset: 0x0000A56F
		internal static string RoutePrefixStartsWithSlash
		{
			get
			{
				return SRResources.ResourceManager.GetString("RoutePrefixStartsWithSlash", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000C385 File Offset: 0x0000A585
		internal static string SelectExpandEmptyOrNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("SelectExpandEmptyOrNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000C39B File Offset: 0x0000A59B
		internal static string SelectionTypeNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("SelectionTypeNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000C3B1 File Offset: 0x0000A5B1
		internal static string SelectNonStructured
		{
			get
			{
				return SRResources.ResourceManager.GetString("SelectNonStructured", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000C3C7 File Offset: 0x0000A5C7
		internal static string SingleResultHasMoreThanOneEntity
		{
			get
			{
				return SRResources.ResourceManager.GetString("SingleResultHasMoreThanOneEntity", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000C3DD File Offset: 0x0000A5DD
		internal static string SingletonAlreadyConfiguredDifferentEntityType
		{
			get
			{
				return SRResources.ResourceManager.GetString("SingletonAlreadyConfiguredDifferentEntityType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000C3F3 File Offset: 0x0000A5F3
		internal static string SingletonNameAlreadyConfiguredAsEntitySet
		{
			get
			{
				return SRResources.ResourceManager.GetString("SingletonNameAlreadyConfiguredAsEntitySet", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000C409 File Offset: 0x0000A609
		internal static string SkipTopLimitExceeded
		{
			get
			{
				return SRResources.ResourceManager.GetString("SkipTopLimitExceeded", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000C41F File Offset: 0x0000A61F
		internal static string SkipTokenParseError
		{
			get
			{
				return SRResources.ResourceManager.GetString("SkipTokenParseError", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000C435 File Offset: 0x0000A635
		internal static string TargetEntityTypeMissing
		{
			get
			{
				return SRResources.ResourceManager.GetString("TargetEntityTypeMissing", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000C44B File Offset: 0x0000A64B
		internal static string TargetKindNotImplemented
		{
			get
			{
				return SRResources.ResourceManager.GetString("TargetKindNotImplemented", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000C461 File Offset: 0x0000A661
		internal static string TypeCannotBeComplexWasEntity
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeCannotBeComplexWasEntity", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000C477 File Offset: 0x0000A677
		internal static string TypeCannotBeDeserialized
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeCannotBeDeserialized", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000C48D File Offset: 0x0000A68D
		internal static string TypeCannotBeEntityWasComplex
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeCannotBeEntityWasComplex", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000C4A3 File Offset: 0x0000A6A3
		internal static string TypeCannotBeEnum
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeCannotBeEnum", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000C4B9 File Offset: 0x0000A6B9
		internal static string TypeCannotBeSerialized
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeCannotBeSerialized", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000C4CF File Offset: 0x0000A6CF
		internal static string TypeDoesNotInheritFromBaseType
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeDoesNotInheritFromBaseType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000C4E5 File Offset: 0x0000A6E5
		internal static string TypeMustBeEntity
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeMustBeEntity", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000C4FB File Offset: 0x0000A6FB
		internal static string TypeMustBeEnumOrNullableEnum
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeMustBeEnumOrNullableEnum", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000C511 File Offset: 0x0000A711
		internal static string TypeMustBeResourceSet
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeMustBeResourceSet", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000C527 File Offset: 0x0000A727
		internal static string TypeOfDynamicPropertyNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeOfDynamicPropertyNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000C53D File Offset: 0x0000A73D
		internal static string UnableToDetermineBaseUrl
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnableToDetermineBaseUrl", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000C553 File Offset: 0x0000A753
		internal static string UnableToDetermineMetadataUrl
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnableToDetermineMetadataUrl", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000C569 File Offset: 0x0000A769
		internal static string UnaryNodeValidationNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnaryNodeValidationNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000C57F File Offset: 0x0000A77F
		internal static string UnexpectedElementType
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnexpectedElementType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000C595 File Offset: 0x0000A795
		internal static string UnresolvedPathSegmentInTemplate
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnresolvedPathSegmentInTemplate", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000C5AB File Offset: 0x0000A7AB
		internal static string UnsupportedEdmType
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnsupportedEdmType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000C5C1 File Offset: 0x0000A7C1
		internal static string UnsupportedEdmTypeKind
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnsupportedEdmTypeKind", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000C5D7 File Offset: 0x0000A7D7
		internal static string UnsupportedExpressionNodeType
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnsupportedExpressionNodeType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000C5ED File Offset: 0x0000A7ED
		internal static string UnsupportedExpressionNodeTypeWithName
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnsupportedExpressionNodeTypeWithName", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000C603 File Offset: 0x0000A803
		internal static string UnsupportedSelectExpandPath
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnsupportedSelectExpandPath", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000C619 File Offset: 0x0000A819
		internal static string InvalidSegmentInSelectExpandPath
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidSegmentInSelectExpandPath", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000C62F File Offset: 0x0000A82F
		internal static string InvalidLastSegmentInSelectExpandPath
		{
			get
			{
				return SRResources.ResourceManager.GetString("InvalidLastSegmentInSelectExpandPath", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000C645 File Offset: 0x0000A845
		internal static string UnterminatedStringLiteral
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnterminatedStringLiteral", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000C65B File Offset: 0x0000A85B
		internal static string UriFunctionClrBinderAlreadyBound
		{
			get
			{
				return SRResources.ResourceManager.GetString("UriFunctionClrBinderAlreadyBound", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000C671 File Offset: 0x0000A871
		internal static string UriQueryStringInvalid
		{
			get
			{
				return SRResources.ResourceManager.GetString("UriQueryStringInvalid", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000C687 File Offset: 0x0000A887
		internal static string UrlHelperNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("UrlHelperNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000C69D File Offset: 0x0000A89D
		internal static string ValueIsInvalid
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValueIsInvalid", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000C6B3 File Offset: 0x0000A8B3
		internal static string WriteObjectInlineNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("WriteObjectInlineNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000C6C9 File Offset: 0x0000A8C9
		internal static string WriteObjectNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("WriteObjectNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000C6DF File Offset: 0x0000A8DF
		internal static string WriteToStreamAsyncMustHaveRequest
		{
			get
			{
				return SRResources.ResourceManager.GetString("WriteToStreamAsyncMustHaveRequest", SRResources.resourceCulture);
			}
		}

		// Token: 0x040000BF RID: 191
		private static ResourceManager resourceMan;

		// Token: 0x040000C0 RID: 192
		private static CultureInfo resourceCulture;
	}
}
