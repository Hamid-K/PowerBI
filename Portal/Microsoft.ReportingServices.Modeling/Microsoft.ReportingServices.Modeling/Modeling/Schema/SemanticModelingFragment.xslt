<?xml version="1.0" encoding="utf-8" ?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
  xmlns:xsd="http://www.w3.org/2001/XMLSchema">

  <xsl:param name="allowReplacementTokens" select="false()" />

  <xsl:output method="xml" indent="yes" encoding="utf-8" />

  <xsl:template name="ApplyAll">
    <xsl:apply-templates select="@*|node()"/>
  </xsl:template>
  <xsl:template name="CopyContent">
    <xsl:copy>
      <xsl:call-template name="ApplyAll" />
    </xsl:copy>
  </xsl:template>
  <xsl:template name="OptionalAttribute">
    <xsl:copy>
      <xsl:call-template name="ApplyAll" />
      <xsl:attribute name="use">optional</xsl:attribute>
    </xsl:copy>
  </xsl:template>
  <xsl:template name="OptionalElement">
    <xsl:copy>
      <!-- 
        Include this before ApplyAll because it cannot be applied after creating nested elements.
        However, this assumes that minOccurs is not specified in the source element (if specified, 
        the source value will overwrite this attribute).
      -->
      <xsl:attribute name="minOccurs">0</xsl:attribute>
      <xsl:call-template name="ApplyAll" />
    </xsl:copy>
  </xsl:template>

  <!-- Exclude top-level schema elements unless explicitly copied below -->
  <xsl:template match="/xsd:schema/*" priority="-1" />
  <!-- Copy other content by default -->
  <xsl:template match="@*|node()" priority="-2">
    <xsl:call-template name="CopyContent" />
  </xsl:template>

  <!-- Make ID attributes optional -->
  <xsl:template match="xsd:attribute[@name='ID']">
    <xsl:call-template name="OptionalAttribute" />
  </xsl:template>

  <!-- Exclude binding elements -->
  <xsl:template match="xsd:element[@name='Table'] |
                       xsd:element[@name='Column'] |
                       xsd:element[@name='Relation']" />

  <!-- Copy these elements as-is -->
  <xsl:template match="xsd:simpleType[@name='NonEmptyString'] |
                       xsd:complexType[@name='CustomPropertiesType'] |
                       xsd:complexType[@name='CustomPropertyType'] |
                       xsd:complexType[@name='EntitiesType'] |
                       xsd:complexType[@name='AttributeReferencesType'] |
                       xsd:complexType[@name='AttributeReferenceType'] |
                       xsd:complexType[@name='SortAttributesType'] |
                       xsd:complexType[@name='SortAttributeType'] |
                       xsd:simpleType[@name='SortAttributeDirectionEnum'] |
                       xsd:complexType[@name='DefaultSecurityFilterType'] |
                       xsd:complexType[@name='FieldsType'] |
                       xsd:complexType[@name='VariationsType'] |
                       xsd:simpleType[@name='DataTypeEnum'] |
                       xsd:simpleType[@name='SortDirectionEnum'] |
                       xsd:simpleType[@name='AttributeContextualNameEnum'] |
                       xsd:complexType[@name='LinguisticsType'] |
                       xsd:complexType[@name='ExpressionType'] |
                       xsd:complexType[@name='ExpressionsType'] |
                       xsd:complexType[@name='PathType'] |
                       xsd:complexType[@name='RolePathItemType'] |
                       xsd:complexType[@name='FunctionType'] |
                       xsd:complexType[@name='AttributeRefType'] |
                       xsd:complexType[@name='EntityRefType'] |
                       xsd:complexType[@name='ParameterRefType'] |
                       xsd:complexType[@name='LiteralType'] |
                       xsd:simpleType[@name='LiteralDataTypeEnum'] |
                       xsd:complexType[@name='NullType']">
    <xsl:call-template name="CopyContent" />
  </xsl:template>

  <!-- Specific rules for ModelItem fragments -->

  <xsl:template match="xsd:complexType[@name='EntityFolderType']">
    <xsl:call-template name="CopyContent" />
    <xsd:element name="EntityFolder" type="EntityFolderType" />
  </xsl:template>

  <xsl:template match="xsd:complexType[@name='EntityType']">
    <xsl:call-template name="CopyContent" />
    <xsd:element name="Entity" type="EntityType" />
  </xsl:template>
  <xsl:template match="xsd:complexType[@name='EntityType']/xsd:all/xsd:element[@name='Name'] |
                       xsd:complexType[@name='EntityType']/xsd:all/xsd:element[@name='InstanceSelection'] |
                       xsd:complexType[@name='EntityType']/xsd:all/xsd:element[@name='IdentifyingAttributes']">
    <xsl:call-template name="OptionalElement" />
  </xsl:template>

  <xsl:template match="xsd:complexType[@name='FieldFolderType']">
    <xsl:call-template name="CopyContent" />
    <xsd:element name="FieldFolder" type="FieldFolderType" />
  </xsl:template>

  <xsl:template match="xsd:complexType[@name='AttributeType']">
    <xsl:call-template name="CopyContent" />
    <xsd:element name="Attribute" type="AttributeType" />
  </xsl:template>
  <xsl:template match="xsd:complexType[@name='AttributeType']/xsd:all/xsd:element[@name='Name'] |
                       xsd:complexType[@name='AttributeType']/xsd:all/xsd:element[@name='DataType']">
    <xsl:call-template name="OptionalElement" />
  </xsl:template>

  <xsl:template match="xsd:complexType[@name='RoleType']">
    <xsl:call-template name="CopyContent" />
    <xsd:element name="Role" type="RoleType" />
  </xsl:template>
  <xsl:template match="xsd:complexType[@name='RoleType']/xsd:all/xsd:element[@name='Name'] |
                       xsd:complexType[@name='RoleType']/xsd:all/xsd:element[@name='Cardinality']">
    <xsl:call-template name="OptionalElement" />
  </xsl:template>
  <!-- Exclude RelatedRoleID element -->
  <xsl:template match="xsd:complexType[@name='RoleType']/xsd:all/xsd:element[@name='RelatedRoleID']" />

  <!-- Specific rules for Expression fragments -->

  <xsl:template match="xsd:complexType[@name='AttributeRefType']/xsd:all/xsd:element[@name='AttributeID']">
    <xsl:copy>
      <xsl:call-template name="ApplyAll" />
      <xsl:if test="$allowReplacementTokens = true()">
        <!-- Change type to string so that replacement tokens can be used (i.e. {AttributeID}) -->
        <xsl:attribute name="type">xsd:string</xsl:attribute>
      </xsl:if>
    </xsl:copy>
  </xsl:template>

</xsl:transform>