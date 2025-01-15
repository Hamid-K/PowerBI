using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FE5 RID: 4069
	internal sealed class ActiveDirectoryTypeCatalog
	{
		// Token: 0x06006AC8 RID: 27336 RVA: 0x0016FAE4 File Offset: 0x0016DCE4
		public ActiveDirectoryTypeCatalog(IEngineHost host, ActiveDirectoryServiceAccessor service)
		{
			this.host = host;
			this.service = service;
		}

		// Token: 0x06006AC9 RID: 27337 RVA: 0x0016FB38 File Offset: 0x0016DD38
		public bool TryGetObjectCategoryForObject(string distinguishedName, out ActiveDirectoryClassSchema classSchema)
		{
			string text;
			if (!this.TryGetObjectsCategoryName(distinguishedName, out text))
			{
				classSchema = null;
				return false;
			}
			classSchema = this.GetObjectClass(text);
			return true;
		}

		// Token: 0x06006ACA RID: 27338 RVA: 0x0016FB60 File Offset: 0x0016DD60
		public ActiveDirectoryClassSchema GetObjectClass(string className)
		{
			ActiveDirectoryClassSchema activeDirectoryClassSchema;
			if (!this.objectClasses.TryGetValue(className, out activeDirectoryClassSchema))
			{
				HashSet<string> hashSet;
				this.LoadObjectClass(new HashSet<string> { className }, new HashSet<string>(), out hashSet);
				if (!this.objectClasses.TryGetValue(className, out activeDirectoryClassSchema))
				{
					throw ActiveDirectoryExceptions.NewObjectClassCouldNotBeFoundException(this.service.Host, className, this.service.Resource);
				}
			}
			return activeDirectoryClassSchema;
		}

		// Token: 0x06006ACB RID: 27339 RVA: 0x0016FBC8 File Offset: 0x0016DDC8
		public string[] GetObjectClassNames(string objectCategoryName)
		{
			HashSet<string> hashSet = new HashSet<string>();
			this.GetObjectClassNamesCore(objectCategoryName, hashSet);
			return hashSet.ToArray<string>();
		}

		// Token: 0x06006ACC RID: 27340 RVA: 0x0016FBE9 File Offset: 0x0016DDE9
		public ActiveDirectoryAttributeSchema GetAttribute(string attributeName)
		{
			return this.attributes[attributeName];
		}

		// Token: 0x06006ACD RID: 27341 RVA: 0x0016FBF7 File Offset: 0x0016DDF7
		public IEnumerable<string> GetStructuralObjectClasses()
		{
			string text = "(&(objectCategory=classSchema)(|(objectClassCategory=0)(objectClassCategory=1)))";
			SortOption sortOption = new SortOption("lDAPDisplayName", SortDirection.Ascending);
			IEnumerable<ActiveDirectoryServiceSearchResult> enumerable = this.service.FindAll(this.service.SchemaPath, text, sortOption, RowCount.Infinite, new string[] { "lDAPDisplayName" });
			foreach (ActiveDirectoryServiceSearchResult activeDirectoryServiceSearchResult in enumerable)
			{
				yield return activeDirectoryServiceSearchResult.GetSingleValueAttribute<string>("lDAPDisplayName");
			}
			IEnumerator<ActiveDirectoryServiceSearchResult> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06006ACE RID: 27342 RVA: 0x0016FC08 File Offset: 0x0016DE08
		public string[] GetAllObjectClassAttributeNames(string className)
		{
			HashSet<string> hashSet = new HashSet<string>();
			this.GetAllObjectClassesInHierarchy(className, hashSet);
			HashSet<string> hashSet2 = new HashSet<string>();
			foreach (string text in hashSet)
			{
				ActiveDirectoryClassSchema objectClass = this.GetObjectClass(text);
				ActiveDirectoryTypeCatalog.AddRange(hashSet2, objectClass.AttributeNames);
			}
			return hashSet2.ToArray<string>();
		}

		// Token: 0x06006ACF RID: 27343 RVA: 0x0016FC80 File Offset: 0x0016DE80
		private void GetObjectClassNamesCore(string categoryName, HashSet<string> classNames)
		{
			classNames.Add(categoryName);
			foreach (string text in this.GetObjectClass(categoryName).ImmediateParentNames)
			{
				if (!classNames.Contains(text))
				{
					this.GetObjectClassNamesCore(text, classNames);
				}
			}
		}

		// Token: 0x06006AD0 RID: 27344 RVA: 0x0016FCC8 File Offset: 0x0016DEC8
		private bool TryGetObjectsCategoryName(string objectDistinguishedName, out string categoryName)
		{
			if (!this.objectCategoryNames.TryGetValue(objectDistinguishedName, out categoryName))
			{
				ActiveDirectoryServiceSearchResult activeDirectoryServiceSearchResult;
				if (!this.service.TryGetObject(objectDistinguishedName, new string[] { "objectClass" }, out activeDirectoryServiceSearchResult))
				{
					return false;
				}
				string[] multiValueAttribute = activeDirectoryServiceSearchResult.GetMultiValueAttribute<string>("objectClass");
				categoryName = multiValueAttribute[multiValueAttribute.Length - 1];
				this.objectCategoryNames.Add(objectDistinguishedName, categoryName);
			}
			return true;
		}

		// Token: 0x06006AD1 RID: 27345 RVA: 0x0016FD28 File Offset: 0x0016DF28
		private void LoadObjectClass(HashSet<string> classesToLoad, HashSet<string> attributesToLoad, out HashSet<string> failedToLoadAttributes)
		{
			HashSet<string> hashSet = new HashSet<string>();
			failedToLoadAttributes = new HashSet<string>(attributesToLoad);
			List<ActiveDirectoryClassSchema> list = new List<ActiveDirectoryClassSchema>();
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/ActiveDirectory/TypeCatalog/LoadObjectClass", TraceEventType.Information, null))
			{
				string text = ActiveDirectoryTypeCatalog.CreateClassesAttributesFilter(classesToLoad, attributesToLoad);
				hostTrace.Add("Filter", text, true);
				int num = 0;
				foreach (ActiveDirectoryServiceSearchResult activeDirectoryServiceSearchResult in this.service.FindAll(this.service.SchemaPath, text, new SortOption(), new RowCount((long)(classesToLoad.Count + attributesToLoad.Count)), new string[]
				{
					"objectCategory", "lDAPDisplayName", "distinguishedName", "auxiliaryClass", "systemAuxiliaryClass", "subClassOf", "systemFlags", "systemMayContain", "systemMustContain", "mustContain",
					"mayContain", "attributeSyntax", "isSingleValued"
				}))
				{
					if (activeDirectoryServiceSearchResult.GetSingleValueAttribute<string>("objectCategory").Contains("Class-Schema"))
					{
						string singleValueAttribute = activeDirectoryServiceSearchResult.GetSingleValueAttribute<string>("lDAPDisplayName");
						string[] multiValueAttribute = activeDirectoryServiceSearchResult.GetMultiValueAttribute<string>("subClassOf");
						string[] multiValueAttribute2 = activeDirectoryServiceSearchResult.GetMultiValueAttribute<string>("auxiliaryClass");
						string[] multiValueAttribute3 = activeDirectoryServiceSearchResult.GetMultiValueAttribute<string>("systemAuxiliaryClass");
						HashSet<string> hashSet2 = new HashSet<string>();
						ActiveDirectoryTypeCatalog.AddRange(hashSet2, activeDirectoryServiceSearchResult.GetMultiValueAttribute<string>("systemMayContain"));
						ActiveDirectoryTypeCatalog.AddRange(hashSet2, activeDirectoryServiceSearchResult.GetMultiValueAttribute<string>("systemMustContain"));
						ActiveDirectoryTypeCatalog.AddRange(hashSet2, activeDirectoryServiceSearchResult.GetMultiValueAttribute<string>("mustContain"));
						ActiveDirectoryTypeCatalog.AddRange(hashSet2, activeDirectoryServiceSearchResult.GetMultiValueAttribute<string>("mayContain"));
						hashSet2.Remove("tokenGroups");
						hashSet2.Remove("tokenGroupsNoGCAcceptable");
						hashSet2.Remove("tokenGroupsGlobalAndUniversal");
						hashSet2.Remove("");
						string[] array = ActiveDirectoryTypeCatalog.JoinArrays(new string[][] { multiValueAttribute, multiValueAttribute2, multiValueAttribute3 });
						string[] array2 = hashSet2.ToArray<string>();
						Array.Sort<string>(array2);
						ActiveDirectoryClassSchema activeDirectoryClassSchema = new ActiveDirectoryClassSchema(singleValueAttribute, array, array2);
						this.objectClasses.Add(singleValueAttribute, activeDirectoryClassSchema);
						list.Add(activeDirectoryClassSchema);
						ActiveDirectoryTypeCatalog.AddRange(hashSet, array);
						ActiveDirectoryTypeCatalog.AddRange(attributesToLoad, hashSet2);
					}
					else
					{
						string singleValueAttribute2 = activeDirectoryServiceSearchResult.GetSingleValueAttribute<string>("lDAPDisplayName");
						bool singleValueAttribute3 = activeDirectoryServiceSearchResult.GetSingleValueAttribute<bool>("isSingleValued");
						string singleValueAttribute4 = activeDirectoryServiceSearchResult.GetSingleValueAttribute<string>("attributeSyntax");
						int num2;
						activeDirectoryServiceSearchResult.TryGetSingleValueAttribute<int>("systemFlags", out num2);
						ActiveDirectoryAttributeSchema activeDirectoryAttributeSchema;
						if (!this.attributes.TryGetValue(singleValueAttribute2, out activeDirectoryAttributeSchema))
						{
							activeDirectoryAttributeSchema = new ActiveDirectoryAttributeSchema(singleValueAttribute2, singleValueAttribute4, singleValueAttribute3, num2);
							this.attributes.Add(singleValueAttribute2, activeDirectoryAttributeSchema);
						}
						else
						{
							activeDirectoryServiceSearchResult.TryGetSingleValueAttribute<int>("systemFlags", out num2);
							string text2 = null;
							activeDirectoryServiceSearchResult.TryGetSingleValueAttribute<string>("distinguishedName", out text2);
							hostTrace.Add(string.Format(CultureInfo.InvariantCulture, "DuplicateAttribute.{0}", num), string.Format(CultureInfo.InvariantCulture, "LdapDisplayName: '{0}' DistinguishedName:'{1}', (syntax, systemFlags, isSingleValued) for new: ('{2}', '{3}', '{4}'); for existing: ('{5}', '{6}', '{7}')", new object[] { singleValueAttribute2, text2, singleValueAttribute4, num2, singleValueAttribute3, activeDirectoryAttributeSchema.Syntax, activeDirectoryAttributeSchema.IsConstructed, activeDirectoryAttributeSchema.IsSingleValued }), true);
							num++;
						}
					}
				}
				failedToLoadAttributes.RemoveWhere(new Predicate<string>(this.attributes.ContainsKey));
				if (failedToLoadAttributes.Any<string>())
				{
					hostTrace.Add("FailedToLoadAttributes", string.Join(",", failedToLoadAttributes.ToArray<string>()), true);
					attributesToLoad.RemoveWhere(new Predicate<string>(failedToLoadAttributes.Contains));
				}
			}
			hashSet.RemoveWhere(new Predicate<string>(this.objectClasses.ContainsKey));
			attributesToLoad.RemoveWhere(new Predicate<string>(this.attributes.ContainsKey));
			if (hashSet.Count > 0 || attributesToLoad.Count > 0)
			{
				HashSet<string> hashSet3;
				this.LoadObjectClass(hashSet, attributesToLoad, out hashSet3);
				if (hashSet3.Any<string>())
				{
					foreach (ActiveDirectoryClassSchema activeDirectoryClassSchema2 in list)
					{
						string[] array3 = activeDirectoryClassSchema2.AttributeNames.Except(hashSet3).ToArray<string>();
						if (array3.Length < activeDirectoryClassSchema2.AttributeNames.Length)
						{
							this.objectClasses[activeDirectoryClassSchema2.Name] = new ActiveDirectoryClassSchema(activeDirectoryClassSchema2.Name, activeDirectoryClassSchema2.ImmediateParentNames, array3);
						}
					}
				}
			}
		}

		// Token: 0x06006AD2 RID: 27346 RVA: 0x00170214 File Offset: 0x0016E414
		private void GetAllObjectClassesInHierarchy(string className, HashSet<string> parents)
		{
			parents.Add(className);
			foreach (string text in this.GetObjectClass(className).ImmediateParentNames)
			{
				if (!parents.Contains(text))
				{
					this.GetAllObjectClassesInHierarchy(text, parents);
					parents.Add(text);
				}
			}
		}

		// Token: 0x06006AD3 RID: 27347 RVA: 0x00170264 File Offset: 0x0016E464
		private static string[] JoinArrays(params string[][] arrays)
		{
			int num = 0;
			foreach (string[] array in arrays)
			{
				num += array.Length;
			}
			string[] array2 = new string[num];
			int num2 = 0;
			foreach (string[] array3 in arrays)
			{
				foreach (string text in array3)
				{
					array2[num2++] = text;
				}
			}
			return array2;
		}

		// Token: 0x06006AD4 RID: 27348 RVA: 0x001702DC File Offset: 0x0016E4DC
		private static void AddRange(HashSet<string> set, IEnumerable<string> values)
		{
			foreach (string text in values)
			{
				set.Add(text);
			}
		}

		// Token: 0x06006AD5 RID: 27349 RVA: 0x00170328 File Offset: 0x0016E528
		private static string CreateClassesAttributesFilter(HashSet<string> classes, HashSet<string> attributes)
		{
			bool flag = attributes.Count > 0 && classes.Count > 0;
			StringBuilder stringBuilder = new StringBuilder();
			if (flag)
			{
				stringBuilder.Append("(|");
			}
			if (classes.Count > 0)
			{
				stringBuilder.Append("(|");
				foreach (string text in classes)
				{
					stringBuilder.Append("(&(objectCategory=classSchema)(lDAPDisplayName=");
					stringBuilder.Append(AttributeValue.Escape(text));
					stringBuilder.Append("))");
				}
				stringBuilder.Append(")");
			}
			if (attributes.Count > 0)
			{
				stringBuilder.Append("(|");
				foreach (string text2 in attributes)
				{
					stringBuilder.Append("(&(objectCategory=attributeSchema)(lDAPDisplayName=");
					stringBuilder.Append(AttributeValue.Escape(text2));
					stringBuilder.Append("))");
				}
				stringBuilder.Append(")");
			}
			if (flag)
			{
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04003B5F RID: 15199
		private readonly ActiveDirectoryServiceAccessor service;

		// Token: 0x04003B60 RID: 15200
		private readonly IEngineHost host;

		// Token: 0x04003B61 RID: 15201
		private readonly Dictionary<string, ActiveDirectoryClassSchema> objectClasses = new Dictionary<string, ActiveDirectoryClassSchema>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04003B62 RID: 15202
		private readonly Dictionary<string, ActiveDirectoryAttributeSchema> attributes = new Dictionary<string, ActiveDirectoryAttributeSchema>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04003B63 RID: 15203
		private readonly Dictionary<string, string> objectCategoryNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
	}
}
