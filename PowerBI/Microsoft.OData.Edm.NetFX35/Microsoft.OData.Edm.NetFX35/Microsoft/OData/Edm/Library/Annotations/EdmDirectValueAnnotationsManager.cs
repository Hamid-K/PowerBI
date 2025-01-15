using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;

namespace Microsoft.OData.Edm.Library.Annotations
{
	// Token: 0x0200007A RID: 122
	public class EdmDirectValueAnnotationsManager : IEdmDirectValueAnnotationsManager
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00004BC5 File Offset: 0x00002DC5
		public EdmDirectValueAnnotationsManager()
		{
			this.annotationsDictionary = VersioningDictionary<IEdmElement, object>.Create(new Func<IEdmElement, IEdmElement, int>(this.CompareElements));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00004ED0 File Offset: 0x000030D0
		public IEnumerable<IEdmDirectValueAnnotation> GetDirectValueAnnotations(IEdmElement element)
		{
			VersioningDictionary<IEdmElement, object> annotationsDictionary = this.annotationsDictionary;
			IEnumerable<IEdmDirectValueAnnotation> immutableAnnotations = this.GetAttachedAnnotations(element);
			object transientAnnotations = EdmDirectValueAnnotationsManager.GetTransientAnnotations(element, annotationsDictionary);
			if (immutableAnnotations != null)
			{
				foreach (IEdmDirectValueAnnotation existingAnnotation in immutableAnnotations)
				{
					if (!EdmDirectValueAnnotationsManager.IsDead(existingAnnotation.NamespaceUri, existingAnnotation.Name, transientAnnotations))
					{
						yield return existingAnnotation;
					}
				}
			}
			foreach (IEdmDirectValueAnnotation existingAnnotation2 in EdmDirectValueAnnotationsManager.TransientAnnotations(transientAnnotations))
			{
				yield return existingAnnotation2;
			}
			yield break;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00004EF4 File Offset: 0x000030F4
		public void SetAnnotationValue(IEdmElement element, string namespaceName, string localName, object value)
		{
			lock (this.annotationsDictionaryLock)
			{
				VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
				this.SetAnnotationValue(element, namespaceName, localName, value, ref versioningDictionary);
				this.annotationsDictionary = versioningDictionary;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00004F44 File Offset: 0x00003144
		public void SetAnnotationValues(IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			lock (this.annotationsDictionaryLock)
			{
				VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
				foreach (IEdmDirectValueAnnotationBinding edmDirectValueAnnotationBinding in annotations)
				{
					this.SetAnnotationValue(edmDirectValueAnnotationBinding.Element, edmDirectValueAnnotationBinding.NamespaceUri, edmDirectValueAnnotationBinding.Name, edmDirectValueAnnotationBinding.Value, ref versioningDictionary);
				}
				this.annotationsDictionary = versioningDictionary;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00004FD8 File Offset: 0x000031D8
		public object GetAnnotationValue(IEdmElement element, string namespaceName, string localName)
		{
			VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
			return this.GetAnnotationValue(element, namespaceName, localName, versioningDictionary);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00004FF8 File Offset: 0x000031F8
		public object[] GetAnnotationValues(IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
			object[] array = new object[Enumerable.Count<IEdmDirectValueAnnotationBinding>(annotations)];
			int num = 0;
			foreach (IEdmDirectValueAnnotationBinding edmDirectValueAnnotationBinding in annotations)
			{
				array[num++] = this.GetAnnotationValue(edmDirectValueAnnotationBinding.Element, edmDirectValueAnnotationBinding.NamespaceUri, edmDirectValueAnnotationBinding.Name, versioningDictionary);
			}
			return array;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005074 File Offset: 0x00003274
		protected virtual IEnumerable<IEdmDirectValueAnnotation> GetAttachedAnnotations(IEdmElement element)
		{
			return null;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000050A8 File Offset: 0x000032A8
		private static void SetAnnotation(IEnumerable<IEdmDirectValueAnnotation> immutableAnnotations, ref object transientAnnotations, string namespaceName, string localName, object value)
		{
			bool flag = false;
			if (immutableAnnotations != null)
			{
				if (Enumerable.Any<IEdmDirectValueAnnotation>(immutableAnnotations, (IEdmDirectValueAnnotation existingAnnotation) => existingAnnotation.NamespaceUri == namespaceName && existingAnnotation.Name == localName))
				{
					flag = true;
				}
			}
			if (value == null && !flag)
			{
				EdmDirectValueAnnotationsManager.RemoveTransientAnnotation(ref transientAnnotations, namespaceName, localName);
				return;
			}
			if (namespaceName == "http://schemas.microsoft.com/ado/2011/04/edm/documentation" && value != null && !(value is IEdmDocumentation))
			{
				throw new InvalidOperationException(Strings.Annotations_DocumentationPun(value.GetType().Name));
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

		// Token: 0x060001EB RID: 491 RVA: 0x00005260 File Offset: 0x00003460
		private static IEdmDirectValueAnnotation FindTransientAnnotation(object transientAnnotations, string namespaceName, string localName)
		{
			if (transientAnnotations != null)
			{
				IEdmDirectValueAnnotation edmDirectValueAnnotation = transientAnnotations as IEdmDirectValueAnnotation;
				if (edmDirectValueAnnotation == null)
				{
					VersioningList<IEdmDirectValueAnnotation> versioningList = (VersioningList<IEdmDirectValueAnnotation>)transientAnnotations;
					return Enumerable.FirstOrDefault<IEdmDirectValueAnnotation>(versioningList, (IEdmDirectValueAnnotation existingAnnotation) => existingAnnotation.NamespaceUri == namespaceName && existingAnnotation.Name == localName);
				}
				if (edmDirectValueAnnotation.NamespaceUri == namespaceName && edmDirectValueAnnotation.Name == localName)
				{
					return edmDirectValueAnnotation;
				}
			}
			return null;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000052D8 File Offset: 0x000034D8
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
								transientAnnotations = Enumerable.Single<IEdmDirectValueAnnotation>(versioningList);
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

		// Token: 0x060001ED RID: 493 RVA: 0x0000557C File Offset: 0x0000377C
		private static IEnumerable<IEdmDirectValueAnnotation> TransientAnnotations(object transientAnnotations)
		{
			if (transientAnnotations != null)
			{
				IEdmDirectValueAnnotation singleAnnotation = transientAnnotations as IEdmDirectValueAnnotation;
				if (singleAnnotation != null)
				{
					if (singleAnnotation.Value != null)
					{
						yield return singleAnnotation;
					}
				}
				else
				{
					VersioningList<IEdmDirectValueAnnotation> annotationsList = (VersioningList<IEdmDirectValueAnnotation>)transientAnnotations;
					foreach (IEdmDirectValueAnnotation existingAnnotation in annotationsList)
					{
						if (existingAnnotation.Value != null)
						{
							yield return existingAnnotation;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00005599 File Offset: 0x00003799
		private static bool IsDead(string namespaceName, string localName, object transientAnnotations)
		{
			return EdmDirectValueAnnotationsManager.FindTransientAnnotation(transientAnnotations, namespaceName, localName) != null;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000055AC File Offset: 0x000037AC
		private static object GetTransientAnnotations(IEdmElement element, VersioningDictionary<IEdmElement, object> annotationsDictionary)
		{
			object obj;
			annotationsDictionary.TryGetValue(element, out obj);
			return obj;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000055C4 File Offset: 0x000037C4
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

		// Token: 0x060001F1 RID: 497 RVA: 0x00005600 File Offset: 0x00003800
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

		// Token: 0x060001F2 RID: 498 RVA: 0x00005694 File Offset: 0x00003894
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
				int num = string.Compare(edmNamedElement.Name, edmNamedElement2.Name, 4);
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
				lock (this.unsortedElementsLock)
				{
					this.unsortedElements = this.unsortedElements.Add(left);
					continue;
				}
				break;
			}
			int num2;
			return num2;
		}

		// Token: 0x040000B0 RID: 176
		private VersioningDictionary<IEdmElement, object> annotationsDictionary;

		// Token: 0x040000B1 RID: 177
		private object annotationsDictionaryLock = new object();

		// Token: 0x040000B2 RID: 178
		private VersioningList<IEdmElement> unsortedElements = VersioningList<IEdmElement>.Create();

		// Token: 0x040000B3 RID: 179
		private object unsortedElementsLock = new object();
	}
}
