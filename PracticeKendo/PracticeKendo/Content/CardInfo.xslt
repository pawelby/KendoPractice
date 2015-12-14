<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>

  <xsl:template match="/">
    <xsl:apply-templates/>
  </xsl:template>

  <xsl:template match="CardInfo">
    <div class="row">
      <div class="col-md-6">
        <strong>Название карты</strong>
      </div>
      <div class="col-md-6 text-right">
        <strong>
          <xsl:value-of select="CardProdName"/>
        </strong>
      </div>
    </div>
    <div class="row">
      <div class="col-md-6">
        <strong>Номер карты</strong>
      </div>
      <div class="col-md-6 text-right">
        <strong>
          <xsl:value-of select="CardNumber"/>
        </strong>
      </div>
    </div>
    <div class="row">
      <div class="col-md-12">&#160;</div>
    </div>
    <div class="row">
      <div class="col-md-6">Валюта:</div>
      <div class="col-md-6 text-right">
        <xsl:value-of select="CardCurISO"/>
      </div>
    </div>
    <div class="row">
      <div class="col-md-6">Имя счета:</div>
      <div class="col-md-6 text-right">
        <xsl:value-of select="AccTypeName"/>
      </div>
    </div>
    <div class="row">
      <div class="col-md-6">Дата выпуска карты:</div>
      <div class="col-md-6 text-right">
        <xsl:value-of select="CardOpenDate"/>
      </div>
    </div>
    <div class="row">
      <div class="col-md-6">Срок окончания действия карты:</div>
      <div class="col-md-6 text-right">
        <xsl:value-of select="CardTermDate"/>
      </div>
    </div>
    <div class="row">
      <div class="col-md-6">Статус карты:</div>
      <div class="col-md-6 text-right">
        <xsl:value-of select="CardStatusName"/>
      </div>
    </div>
    <div class="row">
      <div class="col-md-6">ФИО владельца карт-счета:</div>
      <div class="col-md-6 text-right">
        <xsl:value-of select="CardHolder"/>
      </div>
    </div>
  </xsl:template>
  <xsl:template match="CardTransactions">
  </xsl:template>
</xsl:stylesheet>
