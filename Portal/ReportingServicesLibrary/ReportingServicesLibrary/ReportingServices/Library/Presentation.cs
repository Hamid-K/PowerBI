using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200016D RID: 365
	internal sealed class Presentation : IDisposable
	{
		// Token: 0x06000D7D RID: 3453 RVA: 0x00030E24 File Offset: 0x0002F024
		private PackagePart CopyPart(Uri src, Uri dest)
		{
			PackagePart part = this.m_pptx.GetPart(src);
			PackagePart packagePart = this.m_pptx.CreatePart(dest, part.ContentType);
			StreamSupport.CopyStreamUsingBuffer(part.GetStream(), packagePart.GetStream(), 8192);
			return packagePart;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00030E69 File Offset: 0x0002F069
		private PackagePart CopyPart(string src, string dest)
		{
			return this.CopyPart(new Uri(src, UriKind.Relative), new Uri(dest, UriKind.Relative));
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00030E80 File Offset: 0x0002F080
		private PackagePart CreatePartWithContent(Uri dest, Stream content, string contentType)
		{
			if (this.m_pptx.PartExists(dest))
			{
				this.m_pptx.DeletePart(dest);
			}
			PackagePart packagePart = this.m_pptx.CreatePart(dest, contentType);
			StreamSupport.CopyStreamUsingBuffer(content, packagePart.GetStream(), 8192);
			return packagePart;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00030EC8 File Offset: 0x0002F0C8
		private PackagePart CreatePartWithContent(string dest, Stream content, string contentType)
		{
			return this.CreatePartWithContent(new Uri(dest, UriKind.Relative), content, contentType);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00030EDC File Offset: 0x0002F0DC
		public void AddSlidesToPresentation(List<Uri> slides)
		{
			Uri uri = new Uri("/ppt/presentation.xml", UriKind.Relative);
			PackagePart part = this.m_pptx.GetPart(uri);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(part.GetStream());
			XmlNode xmlNode = xmlDocument.GetElementsByTagName("p:sldIdLst")[0];
			int num = 257;
			foreach (Uri uri2 in slides)
			{
				PackageRelationship packageRelationship = part.CreateRelationship(uri2, TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide");
				XmlNode xmlNode2 = xmlDocument.CreateElement("p:sldId", "http://schemas.openxmlformats.org/presentationml/2006/main");
				XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("r:id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
				xmlAttribute.Value = packageRelationship.Id;
				XmlAttribute xmlAttribute2 = xmlDocument.CreateAttribute("id");
				xmlAttribute2.Value = (256 + num).ToString(CultureInfo.InvariantCulture);
				xmlNode2.Attributes.Append(xmlAttribute);
				xmlNode2.Attributes.Append(xmlAttribute2);
				xmlNode.AppendChild(xmlNode2);
				num++;
			}
			xmlDocument.Save(part.GetStream());
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00031014 File Offset: 0x0002F214
		public void UpdateSlide(int slideNum, Stream activeXData, Stream previewImage, out PackagePart activeXBin, out PackagePart image)
		{
			activeXBin = this.CreatePartWithContent(string.Format(CultureInfo.InvariantCulture, "/ppt/activeX/activeX{0}.bin", slideNum), activeXData, "application/vnd.ms-office.activeX");
			previewImage.Position = 0L;
			image = this.CreatePartWithContent(string.Format(CultureInfo.InvariantCulture, "/ppt/media/image{0}.jpeg", slideNum), previewImage, "image/jpeg");
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00031074 File Offset: 0x0002F274
		public Uri AddSlide(int slideNum, Stream activeXData, Stream previewImage)
		{
			PackagePart packagePart;
			PackagePart packagePart2;
			this.UpdateSlide(slideNum, activeXData, previewImage, out packagePart, out packagePart2);
			PackagePart packagePart3 = this.CopyPart("/ppt/activeX/activeX1.xml", string.Format(CultureInfo.InvariantCulture, "/ppt/activeX/activeX{0}.xml", slideNum));
			packagePart3.CreateRelationship(packagePart.Uri, TargetMode.Internal, "http://schemas.microsoft.com/office/2006/relationships/activeXControlBinary", "rId1");
			PackagePart packagePart4 = this.CopyPart("/ppt/drawings/vmlDrawing1.vml", string.Format(CultureInfo.InvariantCulture, "/ppt/drawings/vmlDrawing{0}.vml", slideNum));
			packagePart4.CreateRelationship(packagePart2.Uri, TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image", "rId1");
			PackagePart packagePart5 = this.CopyPart("/ppt/slides/slide1.xml", string.Format(CultureInfo.InvariantCulture, "/ppt/slides/slide{0}.xml", slideNum));
			packagePart5.CreateRelationship(packagePart4.Uri, TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/vmlDrawing", "rId1");
			packagePart5.CreateRelationship(packagePart3.Uri, TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/control", "rId2");
			packagePart5.CreateRelationship(new Uri("../slideLayouts/slideLayout2.xml", UriKind.Relative), TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideLayout", "rId3");
			packagePart5.CreateRelationship(packagePart2.Uri, TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image", "rId4");
			return packagePart5.Uri;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0003118A File Offset: 0x0002F38A
		public Presentation(Stream file)
		{
			this.m_pptx = Package.Open(file, FileMode.Open, FileAccess.ReadWrite);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000311A0 File Offset: 0x0002F3A0
		public void Dispose()
		{
			if (this.m_pptx != null)
			{
				this.m_pptx.Flush();
				this.m_pptx.Close();
			}
		}

		// Token: 0x04000571 RID: 1393
		private const string NsMain = "http://schemas.openxmlformats.org/presentationml/2006/main";

		// Token: 0x04000572 RID: 1394
		private const string NsRelationships = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";

		// Token: 0x04000573 RID: 1395
		private const string RelSlide = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide";

		// Token: 0x04000574 RID: 1396
		private const string RelLayout = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideLayout";

		// Token: 0x04000575 RID: 1397
		private const string RelActiveXBinary = "http://schemas.microsoft.com/office/2006/relationships/activeXControlBinary";

		// Token: 0x04000576 RID: 1398
		private const string RelImage = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image";

		// Token: 0x04000577 RID: 1399
		private const string RelControl = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/control";

		// Token: 0x04000578 RID: 1400
		private const string RelVmlDrawing = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/vmlDrawing";

		// Token: 0x04000579 RID: 1401
		private Package m_pptx;

		// Token: 0x0400057A RID: 1402
		private const int SlideBaseId = 256;
	}
}
