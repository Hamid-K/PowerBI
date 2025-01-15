using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Metadata
{
	// Token: 0x020000F2 RID: 242
	[DebuggerDisplay("{ElementTypeName}")]
	internal sealed class ClientTypeAnnotation
	{
		// Token: 0x06000A24 RID: 2596 RVA: 0x00025944 File Offset: 0x00023B44
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Pending")]
		internal ClientTypeAnnotation(IEdmType edmType, Type type, string qualifiedName, ClientEdmModel model)
		{
			this.EdmType = edmType;
			this.EdmTypeReference = this.EdmType.ToEdmTypeReference(Util.IsNullableType(type));
			this.ElementTypeName = qualifiedName;
			this.ElementType = Nullable.GetUnderlyingType(type) ?? type;
			this.model = model;
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00025995 File Offset: 0x00023B95
		internal bool IsEntityType
		{
			get
			{
				return this.EdmType.TypeKind == EdmTypeKind.Entity;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x000259A5 File Offset: 0x00023BA5
		internal bool IsStructuredType
		{
			get
			{
				return this.EdmType.TypeKind == EdmTypeKind.Entity || this.EdmType.TypeKind == EdmTypeKind.Complex;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x000259C5 File Offset: 0x00023BC5
		internal ClientPropertyAnnotation MediaDataMember
		{
			get
			{
				if (this.isMediaLinkEntry == null)
				{
					this.CheckMediaLinkEntry();
				}
				return this.mediaDataMember;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x000259E0 File Offset: 0x00023BE0
		internal bool IsMediaLinkEntry
		{
			get
			{
				if (this.isMediaLinkEntry == null)
				{
					this.CheckMediaLinkEntry();
				}
				return this.isMediaLinkEntry.Value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00025A00 File Offset: 0x00023C00
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x00025A08 File Offset: 0x00023C08
		internal IEdmTypeReference EdmTypeReference { get; private set; }

		// Token: 0x06000A2B RID: 2603 RVA: 0x00025A11 File Offset: 0x00023C11
		internal IEnumerable<IEdmProperty> EdmProperties()
		{
			if (this.edmPropertyCache == null)
			{
				this.edmPropertyCache = this.DiscoverEdmProperties().ToArray<IEdmProperty>();
			}
			return this.edmPropertyCache;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00025A32 File Offset: 0x00023C32
		internal IEnumerable<ClientPropertyAnnotation> Properties()
		{
			if (this.clientPropertyCache == null)
			{
				this.BuildPropertyCache();
			}
			return this.clientPropertyCache.Values;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00025A4D File Offset: 0x00023C4D
		internal IEnumerable<ClientPropertyAnnotation> PropertiesToSerialize()
		{
			return from p in this.Properties()
				where ClientTypeAnnotation.ShouldSerializeProperty(this, p)
				orderby p.PropertyName
				select p;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00025A8C File Offset: 0x00023C8C
		internal ClientPropertyAnnotation GetProperty(string propertyName, UndeclaredPropertyBehavior undeclaredPropertyBehavior)
		{
			if (this.clientPropertyCache == null)
			{
				this.BuildPropertyCache();
			}
			ClientPropertyAnnotation clientPropertyAnnotation;
			if (!this.clientPropertyCache.TryGetValue(propertyName, out clientPropertyAnnotation))
			{
				string clientPropertyName = ClientTypeUtil.GetClientPropertyName(this.ElementType, propertyName, undeclaredPropertyBehavior);
				if ((string.IsNullOrEmpty(clientPropertyName) || !this.clientPropertyCache.TryGetValue(clientPropertyName, out clientPropertyAnnotation)) && undeclaredPropertyBehavior == UndeclaredPropertyBehavior.ThrowException)
				{
					throw Error.InvalidOperation(Strings.ClientType_MissingProperty(this.ElementTypeName, propertyName));
				}
			}
			return clientPropertyAnnotation;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00025AF4 File Offset: 0x00023CF4
		internal Version GetMetadataVersion()
		{
			if (this.metadataVersion == null)
			{
				Version odataVersion = Util.ODataVersion4;
				WebUtil.RaiseVersion(ref odataVersion, this.ComputeVersionForPropertyCollection(this.EdmProperties(), null));
				this.metadataVersion = odataVersion;
			}
			return this.metadataVersion;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00025B38 File Offset: 0x00023D38
		private static bool ShouldSerializeProperty(ClientTypeAnnotation type, ClientPropertyAnnotation property)
		{
			return !property.IsDictionary && property != type.MediaDataMember && !property.IsStreamLinkProperty && (type.MediaDataMember == null || type.MediaDataMember.MimeTypeProperty != property) && property.PropertyInfo.GetCustomAttributes(typeof(IgnoreClientPropertyAttribute)).Count<Attribute>() == 0;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00025B94 File Offset: 0x00023D94
		private void BuildPropertyCache()
		{
			lock (this)
			{
				if (this.clientPropertyCache == null)
				{
					Dictionary<string, ClientPropertyAnnotation> dictionary = new Dictionary<string, ClientPropertyAnnotation>(StringComparer.Ordinal);
					foreach (IEdmProperty edmProperty in this.EdmProperties())
					{
						dictionary.Add(edmProperty.Name, this.model.GetClientPropertyAnnotation(edmProperty));
					}
					this.clientPropertyCache = dictionary;
				}
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00025C34 File Offset: 0x00023E34
		private void CheckMediaLinkEntry()
		{
			this.isMediaLinkEntry = new bool?(false);
			MediaEntryAttribute mediaEntryAttribute = (MediaEntryAttribute)this.ElementType.GetCustomAttributes(typeof(MediaEntryAttribute), true).SingleOrDefault<object>();
			if (mediaEntryAttribute != null)
			{
				this.isMediaLinkEntry = new bool?(true);
				ClientPropertyAnnotation clientPropertyAnnotation = this.Properties().SingleOrDefault((ClientPropertyAnnotation p) => p.PropertyName == mediaEntryAttribute.MediaMemberName);
				if (clientPropertyAnnotation == null)
				{
					throw Error.InvalidOperation(Strings.ClientType_MissingMediaEntryProperty(this.ElementTypeName, mediaEntryAttribute.MediaMemberName));
				}
				this.mediaDataMember = clientPropertyAnnotation;
			}
			bool flag = this.ElementType.GetCustomAttributes(typeof(HasStreamAttribute), true).Any<object>();
			if (flag)
			{
				this.isMediaLinkEntry = new bool?(true);
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00025CF8 File Offset: 0x00023EF8
		private Version ComputeVersionForPropertyCollection(IEnumerable<IEdmProperty> propertyCollection, HashSet<IEdmType> visitedComplexTypes)
		{
			Version odataVersion = Util.ODataVersion4;
			foreach (IEdmProperty edmProperty in propertyCollection)
			{
				ClientPropertyAnnotation clientPropertyAnnotation = this.model.GetClientPropertyAnnotation(edmProperty);
				if (edmProperty.Type.TypeKind() == EdmTypeKind.Complex && !clientPropertyAnnotation.IsDictionary)
				{
					if (visitedComplexTypes == null)
					{
						visitedComplexTypes = new HashSet<IEdmType>(EqualityComparer<IEdmType>.Default);
					}
					else if (visitedComplexTypes.Contains(edmProperty.Type.Definition))
					{
						continue;
					}
					visitedComplexTypes.Add(edmProperty.Type.Definition);
					WebUtil.RaiseVersion(ref odataVersion, this.ComputeVersionForPropertyCollection(this.model.GetClientTypeAnnotation(edmProperty).EdmProperties(), visitedComplexTypes));
				}
			}
			return odataVersion;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00025DBC File Offset: 0x00023FBC
		private IEnumerable<IEdmProperty> DiscoverEdmProperties()
		{
			IEdmStructuredType edmStructuredType = this.EdmType as IEdmStructuredType;
			if (edmStructuredType != null)
			{
				HashSet<string> propertyNames = new HashSet<string>(StringComparer.Ordinal);
				do
				{
					foreach (IEdmProperty edmProperty in edmStructuredType.DeclaredProperties)
					{
						string name = edmProperty.Name;
						if (!propertyNames.Contains(name))
						{
							propertyNames.Add(name);
							yield return edmProperty;
						}
					}
					IEnumerator<IEdmProperty> enumerator = null;
					edmStructuredType = edmStructuredType.BaseType;
				}
				while (edmStructuredType != null);
				propertyNames = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x040005E2 RID: 1506
		internal readonly IEdmType EdmType;

		// Token: 0x040005E3 RID: 1507
		internal readonly string ElementTypeName;

		// Token: 0x040005E4 RID: 1508
		internal readonly Type ElementType;

		// Token: 0x040005E5 RID: 1509
		private readonly ClientEdmModel model;

		// Token: 0x040005E6 RID: 1510
		private bool? isMediaLinkEntry;

		// Token: 0x040005E7 RID: 1511
		private ClientPropertyAnnotation mediaDataMember;

		// Token: 0x040005E8 RID: 1512
		private Version metadataVersion;

		// Token: 0x040005E9 RID: 1513
		private Dictionary<string, ClientPropertyAnnotation> clientPropertyCache;

		// Token: 0x040005EA RID: 1514
		private IEdmProperty[] edmPropertyCache;
	}
}
