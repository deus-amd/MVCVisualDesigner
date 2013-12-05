﻿<?xml version="1.0" encoding="utf-8"?>
<Dsl xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="8f2ca638-e08f-4b6f-96a1-ea046a25c190" Description="" Name="MVCVisualDesigner" DisplayName="MVC Visual Designer" Namespace="MVCVisualDesigner" MajorVersion="0" Revision="1" ProductName="MVC Visual Designer" CompanyName="Jun Wang" PackageGuid="2318dda0-8eed-4398-b67d-2e85e627224d" PackageNamespace="MVCVisualDesigner" xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel">
  <Classes>
    <DomainClass Id="404ac32b-b3af-4662-bd2e-14f13a17562b" Description="Description for MVCVisualDesigner.VDWidget" Name="VDWidget" DisplayName="VDWidget" InheritanceModifier="Abstract" Namespace="MVCVisualDesigner" GeneratesDoubleDerived="true">
      <Properties>
        <DomainProperty Id="b3bf12d3-bb09-40bd-85d0-a868753c0d3f" Description="Description for MVCVisualDesigner.VDWidget.Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="VDWidget" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>WidgetHasChildren.Children</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="VDWidgetTitle" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>WidgetHasTitle.Title</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective UsesCustomMerge="true">
          <Index>
            <DomainClassMoniker Name="VDSection" />
          </Index>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="7c91a0ff-d5ec-467a-b0b8-705ad9049330" Description="Description for MVCVisualDesigner.VDView" Name="VDView" DisplayName="VDView" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="db6a631c-6a2e-494c-b56d-a4f048c50743" Description="" Name="VDSection" DisplayName="Section" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="VDSectionHead" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>SectionHasHead.Head</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="VDSectionBody" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>SectionHasBody.Body</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="f9e27ebe-1427-48d9-bba0-b1dfa993ee87" Description="Description for MVCVisualDesigner.VDWidgetTitle" Name="VDWidgetTitle" DisplayName="VDWidget Title" Namespace="MVCVisualDesigner" />
    <DomainClass Id="c5bcbb13-6519-4896-9e8e-2cda89a5c3f6" Description="" Name="VDSectionHead" DisplayName="Section Head" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="b39ae87a-6919-4e1d-a310-83dda7754090" Description="" Name="VDSectionBody" DisplayName="Section Body" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="2500aa65-1d54-460f-a7d5-278035a0f8e3" Description="Description for MVCVisualDesigner.VDSeparator" Name="VDSeparator" DisplayName="VDSeparator" InheritanceModifier="Abstract" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="c761d8da-76c8-4ae9-a268-8d81a7f9fd69" Description="Description for MVCVisualDesigner.VDHoriSeparator" Name="VDHoriSeparator" DisplayName="VDHori Separator" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDSeparator" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="733dc445-5f13-4eea-8922-2ac8229684e0" Description="" Name="TopMargin" DisplayName="Top Margin">
          <Type>
            <ExternalTypeMoniker Name="/System/Double" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ee89b0a4-c89c-4397-8c71-bfb58a862f2b" Description="" Name="BottomMargin" DisplayName="Bottom Margin">
          <Type>
            <ExternalTypeMoniker Name="/System/Double" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="913509e8-33d0-462f-aa85-bd5f4148d3e7" Description="Description for MVCVisualDesigner.VDVertSeparator" Name="VDVertSeparator" DisplayName="VDVert Separator" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDSeparator" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="b5931904-4f5c-4409-8189-a7abfb03c28c" Description="" Name="LeftMargin" DisplayName="Left Margin">
          <Type>
            <ExternalTypeMoniker Name="/System/Double" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="89c67701-70d5-4090-8a5e-947b33f38e17" Description="" Name="RightMargin" DisplayName="Right Margin">
          <Type>
            <ExternalTypeMoniker Name="/System/Double" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
  </Classes>
  <Relationships>
    <DomainRelationship Id="8491408d-7160-4a47-9979-e0dab01d52b3" Description="Description for MVCVisualDesigner.WidgetHasChildren" Name="WidgetHasChildren" DisplayName="Widget Has Children" Namespace="MVCVisualDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="223507db-ce6c-4fe1-87aa-33d1adcb6555" Description="Description for MVCVisualDesigner.WidgetHasChildren.SourceVDWidget" Name="SourceVDWidget" DisplayName="Source VDWidget" PropertyName="Children" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Children">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="c0aff184-c06b-475e-b78b-0854c9f15199" Description="Description for MVCVisualDesigner.WidgetHasChildren.TargetVDWidget" Name="TargetVDWidget" DisplayName="Target VDWidget" PropertyName="Parent" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Parent">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="c2d0e6ff-0c0d-4a7d-8ca7-94d2bc2a088c" Description="Description for MVCVisualDesigner.WidgetHasTitle" Name="WidgetHasTitle" DisplayName="Widget Has Title" Namespace="MVCVisualDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="bdb03725-26b0-460b-92ad-c74d0e68d858" Description="Description for MVCVisualDesigner.WidgetHasTitle.VDWidget" Name="VDWidget" DisplayName="VDWidget" PropertyName="Title" Multiplicity="ZeroOne" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Title">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3e997446-1b1d-4306-b258-b373deba3ada" Description="Description for MVCVisualDesigner.WidgetHasTitle.VDWidgetTitle" Name="VDWidgetTitle" DisplayName="VDWidget Title" PropertyName="Widget" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Widget">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidgetTitle" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="60c511f9-a11f-40e7-9cab-542425f90fe3" Description="Description for MVCVisualDesigner.SectionHasHead" Name="SectionHasHead" DisplayName="Section Has Head" Namespace="MVCVisualDesigner" IsEmbedding="true">
      <BaseRelationship>
        <DomainRelationshipMoniker Name="WidgetHasChildren" />
      </BaseRelationship>
      <Source>
        <DomainRole Id="b5841c7c-52eb-4e63-8d04-5826d66991fb" Description="Description for MVCVisualDesigner.SectionHasHead.VDSection" Name="VDSection" DisplayName="VDSection" PropertyName="Head" Multiplicity="ZeroOne" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Head">
          <RolePlayer>
            <DomainClassMoniker Name="VDSection" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3d2e888c-20b9-4c74-83ba-13231124b279" Description="Description for MVCVisualDesigner.SectionHasHead.VDSectionHead" Name="VDSectionHead" DisplayName="VDSection Head" PropertyName="Section" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Section">
          <RolePlayer>
            <DomainClassMoniker Name="VDSectionHead" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="2003e7b1-fa16-47a1-99a1-61d79247a057" Description="Description for MVCVisualDesigner.SectionHasBody" Name="SectionHasBody" DisplayName="Section Has Body" Namespace="MVCVisualDesigner" IsEmbedding="true">
      <BaseRelationship>
        <DomainRelationshipMoniker Name="WidgetHasChildren" />
      </BaseRelationship>
      <Source>
        <DomainRole Id="022e1990-52a9-48bc-ab8a-9c7bb72f9367" Description="Description for MVCVisualDesigner.SectionHasBody.VDSection" Name="VDSection" DisplayName="VDSection" PropertyName="Body" Multiplicity="ZeroOne" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Body">
          <RolePlayer>
            <DomainClassMoniker Name="VDSection" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="988773ea-649c-4f8d-b7e2-05a5c4f55edd" Description="Description for MVCVisualDesigner.SectionHasBody.VDSectionBody" Name="VDSectionBody" DisplayName="VDSection Body" PropertyName="Section" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Section">
          <RolePlayer>
            <DomainClassMoniker Name="VDSectionBody" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="ab5a4903-7e60-4c67-a237-5f61f0654e46" Description="Description for MVCVisualDesigner.SeparatorRefersTopWidget" Name="SeparatorRefersTopWidget" DisplayName="Separator Refers Top Widget" Namespace="MVCVisualDesigner">
      <Source>
        <DomainRole Id="3d142754-17c4-448b-aaa3-261fd13e25c5" Description="Description for MVCVisualDesigner.SeparatorRefersTopWidget.VDHoriSeparator" Name="VDHoriSeparator" DisplayName="VDHori Separator" PropertyName="TopWidget" Multiplicity="ZeroOne" PropertyDisplayName="Top Widget">
          <RolePlayer>
            <DomainClassMoniker Name="VDHoriSeparator" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="968b8f56-745c-4610-b37e-0e34a8060fff" Description="Description for MVCVisualDesigner.SeparatorRefersTopWidget.VDWidget" Name="VDWidget" DisplayName="VDWidget" PropertyName="BottomSeparator" Multiplicity="ZeroOne" PropertyDisplayName="Bottom Separator">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="5fa5d01c-b9ce-45dc-8a43-60139b0e0c7b" Description="Description for MVCVisualDesigner.SeparatorRefersBottomWidget" Name="SeparatorRefersBottomWidget" DisplayName="Separator Refers Bottom Widget" Namespace="MVCVisualDesigner">
      <Source>
        <DomainRole Id="0411c536-af87-467a-b66a-173791f724d0" Description="Description for MVCVisualDesigner.SeparatorRefersBottomWidget.VDHoriSeparator" Name="VDHoriSeparator" DisplayName="VDHori Separator" PropertyName="BottomWidget" Multiplicity="ZeroOne" PropertyDisplayName="Bottom Widget">
          <RolePlayer>
            <DomainClassMoniker Name="VDHoriSeparator" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="6ba23e40-16ee-47d5-a195-ee3939bbcacd" Description="Description for MVCVisualDesigner.SeparatorRefersBottomWidget.VDWidget" Name="VDWidget" DisplayName="VDWidget" PropertyName="TopSeparator" Multiplicity="ZeroOne" PropertyDisplayName="Top Separator">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="2d6b812d-fc42-4b4c-99c5-a66e10169b1a" Description="Description for MVCVisualDesigner.SeparatorRefersLeftWidget" Name="SeparatorRefersLeftWidget" DisplayName="Separator Refers Left Widget" Namespace="MVCVisualDesigner">
      <Source>
        <DomainRole Id="975d491c-2e71-4131-bf82-a4dcc85fc012" Description="Description for MVCVisualDesigner.SeparatorRefersLeftWidget.VDVertSeparator" Name="VDVertSeparator" DisplayName="VDVert Separator" PropertyName="LeftWidget" Multiplicity="ZeroOne" PropertyDisplayName="Left Widget">
          <RolePlayer>
            <DomainClassMoniker Name="VDVertSeparator" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="16ddd36d-f8d5-4e77-9a9f-0e6a2a54cbce" Description="Description for MVCVisualDesigner.SeparatorRefersLeftWidget.VDWidget" Name="VDWidget" DisplayName="VDWidget" PropertyName="RightSeparator" Multiplicity="ZeroOne" PropertyDisplayName="Right Separator">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="f47ff5c7-3744-4c53-af5c-c48cc1f348bc" Description="Description for MVCVisualDesigner.SeparatorRefersRightWidget" Name="SeparatorRefersRightWidget" DisplayName="Separator Refers Right Widget" Namespace="MVCVisualDesigner">
      <Source>
        <DomainRole Id="18c0cf98-a4df-48da-bb8a-4735141d8d42" Description="Description for MVCVisualDesigner.SeparatorRefersRightWidget.VDVertSeparator" Name="VDVertSeparator" DisplayName="VDVert Separator" PropertyName="RightWidget" Multiplicity="ZeroOne" PropertyDisplayName="Right Widget">
          <RolePlayer>
            <DomainClassMoniker Name="VDVertSeparator" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="81a7b620-7815-4c64-b004-154cb2f1804d" Description="Description for MVCVisualDesigner.SeparatorRefersRightWidget.VDWidget" Name="VDWidget" DisplayName="VDWidget" PropertyName="LeftSeparator" Multiplicity="ZeroOne" PropertyDisplayName="Left Separator">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
  </Relationships>
  <Types>
    <ExternalType Name="DateTime" Namespace="System" />
    <ExternalType Name="String" Namespace="System" />
    <ExternalType Name="Int16" Namespace="System" />
    <ExternalType Name="Int32" Namespace="System" />
    <ExternalType Name="Int64" Namespace="System" />
    <ExternalType Name="UInt16" Namespace="System" />
    <ExternalType Name="UInt32" Namespace="System" />
    <ExternalType Name="UInt64" Namespace="System" />
    <ExternalType Name="SByte" Namespace="System" />
    <ExternalType Name="Byte" Namespace="System" />
    <ExternalType Name="Double" Namespace="System" />
    <ExternalType Name="Single" Namespace="System" />
    <ExternalType Name="Guid" Namespace="System" />
    <ExternalType Name="Boolean" Namespace="System" />
    <ExternalType Name="Char" Namespace="System" />
    <ExternalType Name="Image" Namespace="System.Drawing" />
  </Types>
  <Shapes>
    <GeometryShape Id="e3f0af00-12a6-4223-861d-180f9ed7f7c3" Description="Description for MVCVisualDesigner.VDWidgetShape" Name="VDWidgetShape" DisplayName="VDWidget Shape" InheritanceModifier="Abstract" Namespace="MVCVisualDesigner" FixedTooltipText="VDWidget Shape" InitialHeight="1" Geometry="Rectangle">
      <Properties>
        <DomainProperty Id="58f89d0b-35cb-4f92-aecd-348184bff87c" Description="" Name="disabled" DisplayName="Disabled" Kind="CustomStorage" Category="Internal States" GetterAccessModifier="FamilyOrAssembly" SetterAccessModifier="FamilyOrAssembly">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d974e6e0-adbc-4b2c-bc4d-cce5d6d87290" Description="" Name="isPinned" DisplayName="Is Pinned" Category="Internal States" GetterAccessModifier="FamilyOrAssembly" SetterAccessModifier="FamilyOrAssembly">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="29d8d234-51c6-433f-881f-b5b9b722ad5a" Description="" Name="titleText" DisplayName="Title Text" Kind="Calculated" Category="Internal States" GetterAccessModifier="FamilyOrAssembly" SetterAccessModifier="FamilyOrAssembly">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="dbf4b4ff-482e-4779-b81a-4107eecf2d05" Description="" Name="titleIcon" DisplayName="Title Icon" Kind="Calculated" Category="Internal States" GetterAccessModifier="FamilyOrAssembly" SetterAccessModifier="FamilyOrAssembly">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Image" />
          </Type>
        </DomainProperty>
      </Properties>
    </GeometryShape>
    <GeometryShape Id="4842b8a8-4cb9-429d-ad01-a6d400e0676b" Description="" Name="VDSectionShape" DisplayName="VDSection Shape" Namespace="MVCVisualDesigner" FixedTooltipText="VDSection Shape" InitialWidth="5" InitialHeight="3" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <Port Id="407d285a-e203-4917-a395-730505e148c5" Description="" Name="VDWidgetTitlePort" DisplayName="Widget Title Port" Namespace="MVCVisualDesigner" GeneratesDoubleDerived="true" FixedTooltipText="Widget Title Port" TextColor="White" FillColor="DarkBlue" OutlineColor="DarkBlue" InitialWidth="1" InitialHeight="0.18" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle" />
    <GeometryShape Id="36e435cc-7626-4bb3-a4aa-b529b7cdfea4" Description="" Name="VDSectionHeadShape" DisplayName="VDSection Head Shape" Namespace="MVCVisualDesigner" FixedTooltipText="VDSection Head Shape" FillColor="SkyBlue" OutlineColor="Transparent" InitialWidth="5" InitialHeight="0.3" OutlineThickness="0" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <GeometryShape Id="498d7617-e998-4ceb-9cc7-257e76f1d134" Description="" Name="VDSectionBodyShape" DisplayName="VDSection Body Shape" Namespace="MVCVisualDesigner" FixedTooltipText="VDSection Body Shape" FillColor="Gainsboro" OutlineColor="Transparent" InitialWidth="5" InitialHeight="2.7" OutlineThickness="0" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <GeometryShape Id="b44e3be4-49b9-4846-9947-9cb59c525a31" Description="" Name="VDHoriSeparatorShape" DisplayName="Horizontal Separator Shape" Namespace="MVCVisualDesigner" FixedTooltipText="Horizontal Separator Shape" FillColor="Gray" OutlineColor="Transparent" InitialHeight="0.05" OutlineThickness="0" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <GeometryShape Id="07c13f52-86d8-4883-830a-821b16c4bf78" Description="" Name="VDVertSeparatorShape" DisplayName="Vertical Separator Shape" Namespace="MVCVisualDesigner" FixedTooltipText="Vertical Separator Shape" FillColor="Gray" OutlineColor="Transparent" InitialWidth="0.05" InitialHeight="1" OutlineThickness="0" FillGradientMode="None" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
  </Shapes>
  <XmlSerializationBehavior Name="MVCVisualDesignerSerializationBehavior" Namespace="MVCVisualDesigner">
    <ClassData>
      <XmlClassData TypeName="VDDiagram" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDDiagramMoniker" ElementName="vDDiagram" MonikerTypeName="VDDiagramMoniker">
        <DiagramMoniker Name="VDDiagram" />
      </XmlClassData>
      <XmlClassData TypeName="VDWidget" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDWidgetMoniker" ElementName="vDWidget" MonikerTypeName="VDWidgetMoniker">
        <DomainClassMoniker Name="VDWidget" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="children">
            <DomainRelationshipMoniker Name="WidgetHasChildren" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="VDWidget/Name" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="title">
            <DomainRelationshipMoniker Name="WidgetHasTitle" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="VDView" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDViewMoniker" ElementName="vDView" MonikerTypeName="VDViewMoniker">
        <DomainClassMoniker Name="VDView" />
      </XmlClassData>
      <XmlClassData TypeName="VDSection" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionMoniker" ElementName="vDSection" MonikerTypeName="VDSectionMoniker">
        <DomainClassMoniker Name="VDSection" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="head">
            <DomainRelationshipMoniker Name="SectionHasHead" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="body">
            <DomainRelationshipMoniker Name="SectionHasBody" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="WidgetHasChildren" MonikerAttributeName="" SerializeId="true" MonikerElementName="widgetHasChildrenMoniker" ElementName="widgetHasChildren" MonikerTypeName="WidgetHasChildrenMoniker">
        <DomainRelationshipMoniker Name="WidgetHasChildren" />
      </XmlClassData>
      <XmlClassData TypeName="VDWidgetShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDWidgetShapeMoniker" ElementName="vDWidgetShape" MonikerTypeName="VDWidgetShapeMoniker">
        <GeometryShapeMoniker Name="VDWidgetShape" />
        <ElementData>
          <XmlPropertyData XmlName="disabled">
            <DomainPropertyMoniker Name="VDWidgetShape/disabled" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isPinned">
            <DomainPropertyMoniker Name="VDWidgetShape/isPinned" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="titleText" Representation="Ignore">
            <DomainPropertyMoniker Name="VDWidgetShape/titleText" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="titleIcon" Representation="Ignore">
            <DomainPropertyMoniker Name="VDWidgetShape/titleIcon" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="VDSectionShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionShapeMoniker" ElementName="vDSectionShape" MonikerTypeName="VDSectionShapeMoniker">
        <GeometryShapeMoniker Name="VDSectionShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDWidgetTitle" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDWidgetTitleMoniker" ElementName="vDWidgetTitle" MonikerTypeName="VDWidgetTitleMoniker">
        <DomainClassMoniker Name="VDWidgetTitle" />
      </XmlClassData>
      <XmlClassData TypeName="WidgetHasTitle" MonikerAttributeName="" SerializeId="true" MonikerElementName="widgetHasTitleMoniker" ElementName="widgetHasTitle" MonikerTypeName="WidgetHasTitleMoniker">
        <DomainRelationshipMoniker Name="WidgetHasTitle" />
      </XmlClassData>
      <XmlClassData TypeName="VDWidgetTitlePort" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDWidgetTitlePortMoniker" ElementName="vDWidgetTitlePort" MonikerTypeName="VDWidgetTitlePortMoniker">
        <PortMoniker Name="VDWidgetTitlePort" />
      </XmlClassData>
      <XmlClassData TypeName="VDSectionHead" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionHeadMoniker" ElementName="vDSectionHead" MonikerTypeName="VDSectionHeadMoniker">
        <DomainClassMoniker Name="VDSectionHead" />
      </XmlClassData>
      <XmlClassData TypeName="VDSectionBody" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionBodyMoniker" ElementName="vDSectionBody" MonikerTypeName="VDSectionBodyMoniker">
        <DomainClassMoniker Name="VDSectionBody" />
      </XmlClassData>
      <XmlClassData TypeName="SectionHasHead" MonikerAttributeName="" SerializeId="true" MonikerElementName="sectionHasHeadMoniker" ElementName="sectionHasHead" MonikerTypeName="SectionHasHeadMoniker">
        <DomainRelationshipMoniker Name="SectionHasHead" />
      </XmlClassData>
      <XmlClassData TypeName="SectionHasBody" MonikerAttributeName="" SerializeId="true" MonikerElementName="sectionHasBodyMoniker" ElementName="sectionHasBody" MonikerTypeName="SectionHasBodyMoniker">
        <DomainRelationshipMoniker Name="SectionHasBody" />
      </XmlClassData>
      <XmlClassData TypeName="VDSectionHeadShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionHeadShapeMoniker" ElementName="vDSectionHeadShape" MonikerTypeName="VDSectionHeadShapeMoniker">
        <GeometryShapeMoniker Name="VDSectionHeadShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDSectionBodyShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionBodyShapeMoniker" ElementName="vDSectionBodyShape" MonikerTypeName="VDSectionBodyShapeMoniker">
        <GeometryShapeMoniker Name="VDSectionBodyShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDSeparator" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSeparatorMoniker" ElementName="vDSeparator" MonikerTypeName="VDSeparatorMoniker">
        <DomainClassMoniker Name="VDSeparator" />
      </XmlClassData>
      <XmlClassData TypeName="VDHoriSeparator" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDHoriSeparatorMoniker" ElementName="vDHoriSeparator" MonikerTypeName="VDHoriSeparatorMoniker">
        <DomainClassMoniker Name="VDHoriSeparator" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="topWidget">
            <DomainRelationshipMoniker Name="SeparatorRefersTopWidget" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="bottomWidget">
            <DomainRelationshipMoniker Name="SeparatorRefersBottomWidget" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="topMargin">
            <DomainPropertyMoniker Name="VDHoriSeparator/TopMargin" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="bottomMargin">
            <DomainPropertyMoniker Name="VDHoriSeparator/BottomMargin" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="VDVertSeparator" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDVertSeparatorMoniker" ElementName="vDVertSeparator" MonikerTypeName="VDVertSeparatorMoniker">
        <DomainClassMoniker Name="VDVertSeparator" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="leftWidget">
            <DomainRelationshipMoniker Name="SeparatorRefersLeftWidget" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="rightWidget">
            <DomainRelationshipMoniker Name="SeparatorRefersRightWidget" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="leftMargin">
            <DomainPropertyMoniker Name="VDVertSeparator/LeftMargin" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="rightMargin">
            <DomainPropertyMoniker Name="VDVertSeparator/RightMargin" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="SeparatorRefersTopWidget" MonikerAttributeName="" SerializeId="true" MonikerElementName="separatorRefersTopWidgetMoniker" ElementName="separatorRefersTopWidget" MonikerTypeName="SeparatorRefersTopWidgetMoniker">
        <DomainRelationshipMoniker Name="SeparatorRefersTopWidget" />
      </XmlClassData>
      <XmlClassData TypeName="SeparatorRefersBottomWidget" MonikerAttributeName="" SerializeId="true" MonikerElementName="separatorRefersBottomWidgetMoniker" ElementName="separatorRefersBottomWidget" MonikerTypeName="SeparatorRefersBottomWidgetMoniker">
        <DomainRelationshipMoniker Name="SeparatorRefersBottomWidget" />
      </XmlClassData>
      <XmlClassData TypeName="SeparatorRefersLeftWidget" MonikerAttributeName="" SerializeId="true" MonikerElementName="separatorRefersLeftWidgetMoniker" ElementName="separatorRefersLeftWidget" MonikerTypeName="SeparatorRefersLeftWidgetMoniker">
        <DomainRelationshipMoniker Name="SeparatorRefersLeftWidget" />
      </XmlClassData>
      <XmlClassData TypeName="SeparatorRefersRightWidget" MonikerAttributeName="" SerializeId="true" MonikerElementName="separatorRefersRightWidgetMoniker" ElementName="separatorRefersRightWidget" MonikerTypeName="SeparatorRefersRightWidgetMoniker">
        <DomainRelationshipMoniker Name="SeparatorRefersRightWidget" />
      </XmlClassData>
      <XmlClassData TypeName="VDHoriSeparatorShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDHoriSeparatorShapeMoniker" ElementName="vDHoriSeparatorShape" MonikerTypeName="VDHoriSeparatorShapeMoniker">
        <GeometryShapeMoniker Name="VDHoriSeparatorShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDVertSeparatorShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDVertSeparatorShapeMoniker" ElementName="vDVertSeparatorShape" MonikerTypeName="VDVertSeparatorShapeMoniker">
        <GeometryShapeMoniker Name="VDVertSeparatorShape" />
      </XmlClassData>
    </ClassData>
  </XmlSerializationBehavior>
  <ExplorerBehavior Name="MVCVisualDesignerExplorer" />
  <ConnectionBuilders>
    <ConnectionBuilder Name="SeparatorRefersTopWidgetBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="SeparatorRefersTopWidget" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDHoriSeparator" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDWidget" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="SeparatorRefersBottomWidgetBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="SeparatorRefersBottomWidget" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDHoriSeparator" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDWidget" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="SeparatorRefersLeftWidgetBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="SeparatorRefersLeftWidget" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDVertSeparator" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDWidget" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="SeparatorRefersRightWidgetBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="SeparatorRefersRightWidget" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDVertSeparator" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="VDWidget" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
  </ConnectionBuilders>
  <Diagram Id="1fd11f50-969f-48aa-af5b-31b4277c4671" Description="Description for MVCVisualDesigner.VDDiagram" Name="VDDiagram" DisplayName="Minimal Language Diagram" Namespace="MVCVisualDesigner" GeneratesDoubleDerived="true">
    <Class>
      <DomainClassMoniker Name="VDView" />
    </Class>
    <ShapeMaps>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDSection" />
        <GeometryShapeMoniker Name="VDSectionShape" />
      </ShapeMap>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDWidgetTitle" />
        <ParentElementPath>
          <DomainPath>WidgetHasTitle.Widget/!VDWidget</DomainPath>
        </ParentElementPath>
        <PortMoniker Name="VDWidgetTitlePort" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="VDSectionHead" />
        <ParentElementPath>
          <DomainPath>SectionHasHead.Section/!VDSection</DomainPath>
        </ParentElementPath>
        <GeometryShapeMoniker Name="VDSectionHeadShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="VDSectionBody" />
        <ParentElementPath>
          <DomainPath>SectionHasBody.Section/!VDSection</DomainPath>
        </ParentElementPath>
        <GeometryShapeMoniker Name="VDSectionBodyShape" />
      </ShapeMap>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDHoriSeparator" />
        <GeometryShapeMoniker Name="VDHoriSeparatorShape" />
      </ShapeMap>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDVertSeparator" />
        <GeometryShapeMoniker Name="VDVertSeparatorShape" />
      </ShapeMap>
    </ShapeMaps>
  </Diagram>
  <Designer CopyPasteGeneration="CopyPasteOnly" FileExtension="amvd" EditorGuid="061c971a-6960-4ef9-9c30-8a8a6543b9f7">
    <RootClass>
      <DomainClassMoniker Name="VDView" />
    </RootClass>
    <XmlSerializationDefinition CustomPostLoad="false">
      <XmlSerializationBehaviorMoniker Name="MVCVisualDesignerSerializationBehavior" />
    </XmlSerializationDefinition>
    <ToolboxTab TabText="MVC Visual Designer">
      <ElementTool Name="SectionTool" ToolboxIcon="Resources\SectionIcon.bmp" Caption="Section" Tooltip="" HelpKeyword="SectionTool">
        <DomainClassMoniker Name="VDSection" />
      </ElementTool>
    </ToolboxTab>
    <Validation UsesMenu="false" UsesOpen="false" UsesSave="false" UsesLoad="false" />
    <DiagramMoniker Name="VDDiagram" />
  </Designer>
  <Explorer ExplorerGuid="a88f64c9-f30b-4d4a-825d-009ceed441ad" Title="MVC Visual Designer Explorer">
    <ExplorerBehaviorMoniker Name="MVCVisualDesigner/MVCVisualDesignerExplorer" />
  </Explorer>
</Dsl>