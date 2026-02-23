Imports System.Math
Public Class Seqv
    Inherits Seqt
    ''' <summary>
    ''' 获取或设置粒子的X轴旋转角度列表，<br/>
    ''' 以角度为单位。
    ''' </summary>
    Public AngleX As New List(Of Double)
    ''' <summary>
    ''' 获取或设置粒子的Y轴旋转角度列表，<br/>
    ''' 以角度为单位。
    ''' </summary>
    Public AngleY As New List(Of Double)
    ''' <summary>
    ''' 获取或设置粒子的Z轴旋转角度列表，<br/>
    ''' 以角度为单位。
    ''' </summary>
    Public AngleZ As New List(Of Double)
    ''' <summary>
    ''' 创建新的Seqv粒子
    ''' </summary>
    ''' <param name="Age">寿命</param>
    ''' <param name="Random">寿命随机附加量</param>
    ''' <param name="texture">贴图名称</param>
    Public Sub New(age As Integer, random As Integer, Optional texture As String = "")
        MyBase.New(age, random, texture)
    End Sub
    ''' <summary>
    ''' 创建新的Seqv粒子
    ''' </summary>
    ''' <param name="Age">寿命</param>
    ''' <param name="Random">寿命随机附加量</param>
    ''' <param name="X">X坐标</param>
    ''' <param name="Y">Y坐标</param>
    ''' <param name="Z">Z坐标</param>
    ''' <param name="Relative">是否启用相对坐标</param>
    ''' <param name="texture">贴图名称</param>
    Public Sub New(Age As Integer, Random As Integer, X As Double, Y As Double, Z As Double, Optional Relative As Boolean = True, Optional texture As String = "")
        MyBase.New(Age, Random, X, Y, Z, Relative， texture)
    End Sub
    ''' <summary>
    ''' 将粒子输出为Minecraft命令
    ''' </summary>
    ''' <param name="precision">坐标保留至小数点后几位</param>
    ''' <returns>粒子命令</returns>
    Public Overrides Function Export(Optional precision As Integer = 4) As String
        Caculate()
        Dim command As String = "particle soy:seqv{xlist:["
        Dim replication As Integer
        replication = Xlist.GetReplication
        For i = 0 To replication
            command += Xlist(i).ToString("F" + CStr(precision))
            If i < replication Then
                command += ","
            End If
        Next
        command += "],ylist:["
        replication = Ylist.GetReplication
        For i = 0 To replication
            command += Ylist(i).ToString("F" + CStr(precision))
            If i < replication Then
                command += ","
            End If
        Next
        command += "],zlist:["
        replication = Zlist.GetReplication
        For i = 0 To replication
            command += Zlist(i).ToString("F" + CStr(precision))
            If i < replication Then
                command += ","
            End If
        Next
        command += "],age:" + CStr(Age)
        If Random > 0 Then
            command += ",random:" + CStr(Random)
        End If
        If Color.Count > 0 Then
            command += ",clist:["
            replication = Color.GetReplication
            For i = 0 To replication
                If Color(i).A < 255 Then
                    command += CStr((CType(Color(i).R, Integer) << 16) + (CType(Color(i).G, Integer) << 8) + Color(i).B + (CType(255 - Color(i).A, Integer) << 24))
                Else
                    command += CStr((CType(Color(i).R, Integer) << 16) + (CType(Color(i).G, Integer) << 8) + Color(i).B)
                End If
                If i < replication Then
                    command += ","
                Else
                    command += "]"
                End If
            Next
        End If
        If Size.Count > 0 Then
            command += ",alist:["
            replication = Size.GetReplication
            For i = 0 To replication
                command += Size(i).ToString("F" + CStr(precision))
                If i < replication Then
                    command += ","
                Else
                    command += "]"
                End If
            Next
        End If
        If Light.Count > 0 Then
            command += ",light:["
            replication = Light.GetReplication
            For i = 0 To replication
                command += CStr(Light(i))
                If i < replication Then
                    command += ","
                Else
                    command += "]"
                End If
            Next
        End If
        If Texture <> "" Then
            command += ",texture:""" + Texture + """"
        End If
        If AngleX.Count > 0 Then
            command += ",angleX:["
            replication = AngleX.GetReplication
            For i = 0 To replication
                command += (AngleX(i) / 180 * PI).ToString("F" + CStr(precision))
                If i < replication Then
                    command += ","
                Else
                    command += "]"
                End If
            Next
        End If
        If AngleY.Count > 0 Then
            command += ",angleY:["
            replication = AngleY.GetReplication
            For i = 0 To replication
                command += (AngleY(i) / 180 * PI).ToString("F" + CStr(precision))
                If i < replication Then
                    command += ","
                Else
                    command += "]"
                End If
            Next
        End If
        If AngleZ.Count > 0 Then
            command += ",angleZ:["
            replication = AngleZ.GetReplication
            For i = 0 To replication
                command += (AngleZ(i) / 180 * PI).ToString("F" + CStr(precision))
                If i < replication Then
                    command += ","
                Else
                    command += "]"
                End If
            Next
        End If
        If AngleX.Count > 0 OrElse AngleY.Count > 0 OrElse AngleZ.Count > 0 Then
            command += ",relative:false"
        End If
        command += "}"
        If Relative Then
            command += " ~" + Position.X.ToString("F" + CStr(precision)) + " ~" + Position.Y.ToString("F" + CStr(precision)) + " ~" + Position.Z.ToString("F" + CStr(precision))
        Else
            command += " " + Position.X.ToString("F" + CStr(precision)) + " " + Position.Y.ToString("F" + CStr(precision)) + " " + Position.Z.ToString("F" + CStr(precision))
        End If
        command += " 0 0 0 0 0 force @a " + Tag
        Return command
    End Function
End Class
