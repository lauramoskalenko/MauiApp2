<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output method="html" encoding="UTF-8" indent="yes"/>
	<xsl:template match="/Cadres">
		<html>
			<head>
				<title>Scientific Staff</title>
			</head>
			<body>
				<h1>Scientific Staff Details</h1>
				<table border="1">
					<tr>
						<th>Name</th>
						<th>Faculty</th>
						<th>Department</th>
						<th>Degree</th>
						<th>Rank</th>
						<th>Rank Date</th>
						<th>Publications</th>
					</tr>
					<xsl:for-each select="Scientist">
						<tr>
							<td>
								<xsl:value-of select="@Name"/>
							</td>
							<td>
								<xsl:value-of select="@Faculty"/>
							</td>
							<td>
								<xsl:value-of select="@Department"/>
							</td>
							<td>
								<xsl:value-of select="@Degree"/>
							</td>
							<td>
								<xsl:value-of select="@Rank"/>
							</td>
							<td>
								<xsl:value-of select="@RankDate"/>
							</td>
							<td>
								<ul>
									<xsl:for-each select="Publications/Publication">
										<li>
											<xsl:value-of select="@Title"/> (<xsl:value-of select="@Year"/>)
										</li>
									</xsl:for-each>
								</ul>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
