Imports System.Windows.Media
Imports System.Windows.Media.Media3D
Public Class Seq
    Inherits Particle
    ''' <summary>
    ''' 获取或设置粒子的寿命
    ''' </summary>
    Public Age As Integer
    ''' <summary>
    ''' 获取或设置粒子的寿命随机附加量
    ''' </summary>
    Public Random As Integer
    ''' <summary>
    ''' 获取或设置粒子颜色列表
    ''' </summary>
    Public Color As New List(Of Color)
    ''' <summary>
    ''' 获取或设置粒子的X坐标列表
    ''' </summary>
    Public Xlist As New List(Of Double)
    ''' <summary>
    ''' 获取或设置粒子的Y坐标列表
    ''' </summary>
    Public Ylist As New List(Of Double)
    ''' <summary>
    ''' 获取或设置粒子的Z坐标列表
    ''' </summary>
    Public Zlist As New List(Of Double)
    ''' <summary>
    ''' 获取或设置粒子的尺寸列表
    ''' </summary>
    Public Size As New List(Of Double)
    ''' <summary>
    ''' 获取或设置粒子的亮度列表
    ''' </summary>
    Public Light As New List(Of Integer)
    ''' <summary>
    ''' 获取或设置粒子行为函数
    ''' </summary>
    Public Act As Action
    ''' <summary>
    ''' 表示运算至第几游戏刻<br/>
    ''' 通常由Caculate方法自动完成。
    ''' </summary>
    Public Tick As Integer
    ''' <summary>
    ''' 将速度、加速度、行为函数应用至坐标列表<br/>
    ''' 会在Export方法中自动调用，无需手动调用。
    ''' </summary>
    Public Overridable Sub Caculate()
        Dim t_position As New Point3D
        For Tick = 0 To Age + Random
            t_position += Speed
            If Not IsNothing(Act) Then
                Act()
            End If
            Xlist(Tick) += t_position.X
            Ylist(Tick) += t_position.Y
            Zlist(Tick) += t_position.Z
            Speed.X += Acceleration.X
            Speed.Y += Acceleration.Y
            Speed.Z += Acceleration.Z

        Next
        Speed = New Vector3D(0, 0, 0)
        Acceleration = New Vector3D(0, 0, 0)
        Act = Nothing
    End Sub
    ''' <summary>
    ''' 创建新的Seq粒子
    ''' </summary>
    ''' <param name="Age">寿命</param>
    ''' <param name="Random">寿命随机附加量</param>
    Public Sub New(Age As Integer, Random As Integer)
        Me.Age = Age
        Me.Random = Random
        For i = 0 To Age + Random
            Xlist.Add(0)
            Ylist.Add(0)
            Zlist.Add(0)
        Next
    End Sub
    ''' <summary>
    ''' 创建新的Seq粒子
    ''' </summary>
    ''' <param name="Age">寿命</param>
    ''' <param name="Random">寿命随机附加量</param>
    ''' <param name="X">X坐标</param>
    ''' <param name="Y">Y坐标</param>
    ''' <param name="Z">Z坐标</param>
    ''' <param name="Relative">是否启用相对坐标</param>
    Public Sub New(Age As Integer, Random As Integer, X As Double, Y As Double, Z As Double, Optional Relative As Boolean = True)
        Me.Age = Age
        Me.Random = Random
        Me.Relative = Relative
        Position.X = X
        Position.Y = Y
        Position.Z = Z
        For i = 0 To Age + Random
            Xlist.Add(0)
            Ylist.Add(0)
            Zlist.Add(0)
        Next
    End Sub
    ''' <summary>
    ''' 将粒子输出为Minecraft命令
    ''' </summary>
    ''' <param name="precision">坐标保留至小数点后几位</param>
    ''' <returns>粒子命令</returns>
    Public Overrides Function Export(Optional precision As Integer = 4) As String
        Caculate()
        Dim command As String = "particle soy:seq{xlist:["
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
