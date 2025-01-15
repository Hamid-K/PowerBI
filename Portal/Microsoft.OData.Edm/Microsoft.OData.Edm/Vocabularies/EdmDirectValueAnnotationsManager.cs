using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000DA RID: 218
	public class EdmDirectValueAnnotationsManager : IEdmDirectValueAnnotationsManager
	{
		// Token: 0x060006AA RID: 1706 RVA: 0x0000F964 File Offset: 0x0000DB64
		public EdmDirectValueAnnotationsManager()
		{
			this.annotationsDictionary = VersioningDictionary<IEdmElement, object>.Create(new Func<IEdmElement, IEdmElement, int>(this.CompareElements));
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000F9A4 File Offset: 0x0000DBA4
		public IEnumerable<IEdmDirectValueAnnotation> GetDirectValueAnnotations(IEdmElement element)
		{
			VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
			IEnumerable<IEdmDirectValueAnnotation> attachedAnnotations = this.GetAttachedAnnotations(element);
			object transientAnnotations = EdmDirectValueAnnotationsManager.GetTransientAnnotations(element, versioningDictionary);
			IEnumerator<IEdmDirectValueAnnotation> enumerator;
			if (attachedAnnotations != null)
			{
				foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in attachedAnnotations)
				{
					if (!EdmDirectValueAnnotationsManager.IsDead(edmDirectValueAnnotation.NamespaceUri, edmDirectValueAnnotation.Name, transientAnnotations))
					{
						yield return edmDirectValueAnnotation;
					}
				}
				enumerator = null;
			}
			foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation2 in EdmDirectValueAnnotationsManager.TransientAnnotations(transientAnnotations))
			{
				yield return edmDirectValueAnnotation2;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000F9BC File Offset: 0x0000DBBC
		public void SetAnnotationValue(IEdmElement element, string namespaceName, string localName, object value)
		{
			object obj = this.annotationsDictionaryLock;
			lock (obj)
			{
				VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
				this.SetAnnotationValue(element, namespaceName, localName, value, ref versioningDictionary);
				this.annotationsDictionary = versioningDictionary;
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000FA14 File Offset: 0x0000DC14
		public void SetAnnotationValues(IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			object obj = this.annotationsDictionaryLock;
			lock (obj)
			{
				VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
				foreach (IEdmDirectValueAnnotationBinding edmDirectValueAnnotationBinding in annotations)
				{
					this.SetAnnotationValue(edmDirectValueAnnotationBinding.Element, edmDirectValueAnnotationBinding.NamespaceUri, edmDirectValueAnnotationBinding.Name, edmDirectValueAnnotationBinding.Value, ref versioningDictionary);
				}
				this.annotationsDictionary = versioningDictionary;
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000FAB4 File Offset: 0x0000DCB4
		public object GetAnnotationValue(IEdmElement element, string namespaceName, string localName)
		{
			VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
			return this.GetAnnotationValue(element, namespaceName, localName, versioningDictionary);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
		public object[] GetAnnotationValues(IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
			object[] array = new object[annotations.Count<IEdmDirectValueAnnotationBinding>()];
			int num = 0;
			foreach (IEdmDirectValueAnnotationBinding edmDirectValueAnnotationBinding in annotations)
			{
				array[num++] = this.GetAnnotationValue(edmDirectValueAnnotationBinding.Element, edmDirectValueAnnotationBinding.NamespaceUri, edmDirectValueAnnotationBinding.Name, versioningDictionary);
			}
			return array;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000026B0 File Offset: 0x000008B0
		protected virtual IEnumerable<IEdmDirectValueAnnotation> GetAttachedAnnotations(IEdmElement element)
		{
			return null;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000FB50 File Offset: 0x0000DD50
		private static void SetAnnotation(IEnumerable<IEdmDirectValueAnnotation> immutableAnnotations, ref object transientAnnotations, string namespaceName, string localName, object value)
		{
			bool flag = false;
			if (immutableAnnotations != null && immutableAnnotations.Any((IEdmDirectValueAnnotation existingAnnotation) => existingAnnotation.NamespaceUri == namespaceName && existingAnnotation.Name == localName))
			{
				flag = true;
			}
			if (value == null && !flag)
			{
				EdmDirectValueAnnotationsManager.RemoveTransientAnnotation(ref transientAnnotations, namespaceName, localName);
				return;
			}
			IEdmDirectValueAnnotation edmDirectValueAnnotation = ((value != null) ? new EdmDirectValueAnnotation(namespaceName, localName, value) : new EdmDirectValueAnnotation(namespaceName, localName));
			if (transientAnnotations == null)
			{
				transientAnnotations = edmDirectValueAnnotation;
				return;
			}
			IEdmDirectValueAnnotation edmDirectValueAnnotation2 = transientAnnotations as IEdmDirectValueAnnotation;
			if (edmDirectValueAnnotation2 == null)
			{
				VersioningList<IEdmDirectValueAnnotation> versioningList = (VersioningList<IEdmDirectValueAnnotation>)transientAnnotations;
				for (int i = 0; i < versioningList.Count; i++)
				{
					IEdmDirectValueAnnotation edmDirectValueAnnotation3 = versioningList[i];
					if (edmDirectValueAnnotation3.NamespaceUri == namespaceName && edmDirectValueAnnotation3.Name == localName)
					{
						versioningList = versioningList.RemoveAt(i);
						break;
					}
				}
				transientAnnotations = versioningList.Add(edmDirectValueAnnotation);
				return;
			}
			if (edmDirectValueAnnotation2.NamespaceUri == namespaceName && edmDirectValueAnnotation2.Name == localName)
			{
				transientAnnotations = edmDirectValueAnnotation;
				return;
			}
			transientAnnotations = VersioningList<IEdmDirectValueAnnotation>.Create().Add(edmDirectValueAnnotation2).Add(edmDirectValueAnnotation);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000FC8C File Offset: 0x0000DE8C
		private static IEdmDirectValueAnnotation FindTransientAnnotation(object transientAnnotations, string namespaceName, string localName)
		{
			if (transientAnnotations != null)
			{
				IEdmDirectValueAnnotation edmDirectValueAnnotation = transientAnnotations as IEdmDirectValueAnnotation;
				if (edmDirectValueAnnotation == null)
				{
					VersioningList<IEdmDirectValueAnnotation> versioningList = (VersioningList<IEdmDirectValueAnnotation>)transientAnnotations;
					return versioningList.FirstOrDefault((IEdmDirectValueAnnotation existingAnnotation) => existingAnnotation.NamespaceUri == namespaceName && existingAnnotation.Name == localName);
				}
				if (edmDirectValueAnnotation.NamespaceUri == namespaceName && edmDirectValueAnnotation.Name == localName)
				{
					return edmDirectValueAnnotation;
				}
			}
			return null;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0000FD00 File Offset: 0x0000DF00
		private static void RemoveTransientAnnotation(ref object transientAnnotations, string namespaceName, string localName)
		{
			if (transientAnnotations != null)
			{
				IEdmDirectValueAnnotation edmDirectValueAnnotation = transientAnnotations as IEdmDirectValueAnnotation;
				if (edmDirectValueAnnotation != null)
				{
					if (edmDirectValueAnnotation.NamespaceUri == namespaceName && edmDirectValueAnnotation.Name == localName)
					{
						transientAnnotations = null;
						return;
					}
				}
				else
				{
					VersioningList<IEdmDirectValueAnnotation> versioningList = (VersioningList<IEdmDirectValueAnnotation>)transientAnnotations;
					int i = 0;
					while (i < versioningList.Count)
					{
						IEdmDirectValueAnnotation edmDirectValueAnnotation2 = versioningList[i];
						if (edmDirectValueAnnotation2.NamespaceUri == namespaceName && edmDirectValueAnnotation2.Name == localName)
						{
							versioningList = versioningList.RemoveAt(i);
							if (versioningList.Count == 1)
							{
								transientAnnotations = versioningList.Single<IEdmDirectValueAnnotation>();
								return;
							}
							transientAnnotations = versioningList;
							return;
						}
						else
						{
							i++;
						}
					}
				}
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000FD9A File Offset: 0x0000DF9A
		private static IEnumerable<IEdmDirectValueAnnotation> TransientAnnotations(object transientAnnotations)
		{
			if (transientAnnotations == null)
			{
				yield break;
			}
			IEdmDirectValueAnnotation edmDirectValueAnnotation = transientAnnotations as IEdmDirectValueAnnotation;
			if (edmDirectValueAnnotation != null)
			{
				if (edmDirectValueAnnotation.Value != null)
				{
					yield return edmDirectValueAnnotation;
				}
				yield break;
			}
			VersioningList<IEdmDirectValueAnnotation> versioningList = (VersioningList<IEdmDirectValueAnnotation>)transientAnnotations;
			foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation2 in versioningList)
			{
				if (edmDirectValueAnnotation2.Value != null)
				{
					yield return edmDirectValueAnnotation2;
				}
			}
			IEnumerator<IEdmDirectValueAnnotation> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000FDAA File Offset: 0x0000DFAA
		private static bool IsDead(string namespaceName, string localName, object transientAnnotations)
		{
			return EdmDirectValueAnnotationsManager.FindTransientAnnotation(transientAnnotations, namespaceName, localName) != null;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000FDB8 File Offset: 0x0000DFB8
		private static object GetTransientAnnotations(IEdmElement element, VersioningDictionary<IEdmElement, object> annotationsDictionary)
		{
			object obj;
			annotationsDictionary.TryGetValue(element, out obj);
			return obj;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000FDD0 File Offset: 0x0000DFD0
		private void SetAnnotationValue(IEdmElement element, string namespaceName, string localName, object value, ref VersioningDictionary<IEdmElement, object> annotationsDictionary)
		{
			object transientAnnotations = EdmDirectValueAnnotationsManager.GetTransientAnnotations(element, annotationsDictionary);
			object obj = transientAnnotations;
			EdmDirectValueAnnotationsManager.SetAnnotation(this.GetAttachedAnnotations(element), ref transientAnnotations, namespaceName, localName, value);
			if (transientAnnotations != obj)
			{
				annotationsDictionary = annotationsDictionary.Set(element, transientAnnotations);
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000FE0C File Offset: 0x0000E00C
		private object GetAnnotationValue(IEdmElement element, string namespaceName, string localName, VersioningDictionary<IEdmElement, object> annotationsDictionary)
		{
			IEdmDirectValueAnnotation edmDirectValueAnnotation = EdmDirectValueAnnotationsManager.FindTransientAnnotation(EdmDirectValueAnnotationsManager.GetTransientAnnotations(element, annotationsDictionary), namespaceName, localName);
			if (edmDirectValueAnnotation != null)
			{
				return edmDirectValueAnnotation.Value;
			}
			IEnumerable<IEdmDirectValueAnnotation> attachedAnnotations = this.GetAttachedAnnotations(element);
			if (attachedAnnotations != null)
			{
				foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation2 in attachedAnnotations)
				{
					if (edmDirectValueAnnotation2.NamespaceUri == namespaceName && edmDirectValueAnnotation2.Name == localName)
					{
						return edmDirectValueAnnotation2.Value;
					}
				}
			}
			return null;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000FE9C File Offset: 0x0000E09C
		private int CompareElements(IEdmElement left, IEdmElement right)
		{
			if (left == right)
			{
				return 0;
			}
			int hashCode = left.GetHashCode();
			int hashCode2 = right.GetHashCode();
			if (hashCode < hashCode2)
			{
				return -1;
			}
			if (hashCode > hashCode2)
			{
				return 1;
			}
			IEdmNamedElement edmNamedElement = left as IEdmNamedElement;
			IEdmNamedElement edmNamedElement2 = right as IEdmNamedElement;
			if (edmNamedElement == null)
			{
				if (edmNamedElement2 != null)
				{
					return -1;
				}
			}
			else
			{
				if (edmNamedElement2 == null)
				{
					return 1;
				}
				int num = string.Compare(edmNamedElement.Name, edmNamedElement2.Name, StringComparison.Ordinal);
				if (num != 0)
				{
					return num;
				}
			}
			for (;;)
			{
				foreach (IEdmElement edmElement in this.unsortedElements)
				{
					if (edmElement == left)
					{
						return 1;
					}
					if (edmElement == right)
					{
						return -1;
					}
				}
				object obj = this.unsortedElementsLock;
				lock (obj)
				{
					this.unsortedElements = this.unsortedElements.Add(left);
					continue;
				}
				break;
			}
			int num2;
			return num2;
		}

		// Token: 0x040002E1 RID: 737
		private VersioningDictionary<IEdmElement, object> annotationsDictionary;

		// Token: 0x040002E2 RID: 738
		private object annotationsDictionaryLock = new object();

		// Token: 0x040002E3 RID: 739
		private VersioningList<IEdmElement> unsortedElements = VersioningList<IEdmElement>.Create();

		// Token: 0x040002E4 RID: 740
		private object unsortedElementsLock = new object();
	}
}
