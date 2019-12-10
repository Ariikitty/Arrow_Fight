Public Class Game
    Private Xvel As Single = 0
    Private Yvel As Single = 10
    Private blnKeyLeft As Boolean = False
    Private blnKeyRight As Boolean = False
    Private blnKeyJump As Boolean = False
    Private maxPlatforms As Integer = 3
    Private picPlatforms(maxPlatforms + 1) As PictureBox
    'Runs when the form loads
    Private Sub Game_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        picPlatforms(0) = PictureBox1
        picPlatforms(1) = PictureBox2
        picPlatforms(2) = PictureBox3
        picPlatforms(3) = PictureBox4
    End Sub
    ' Handles key presses
    Private Sub Game_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.D Then
            blnKeyRight = True
            picPlayer.Image = My.Resources.arrow_right
        End If
        If e.KeyCode = Keys.A Then
            blnKeyLeft = True
            picPlayer.Image = My.Resources.arrow_left
        End If
        If e.KeyCode = Keys.Space Then
            blnKeyJump = True
        End If
    End Sub
    ' Handles key releases
    Private Sub Game_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.D Then
            blnKeyRight = False
        End If
        If e.KeyCode = Keys.A Then
            blnKeyLeft = False
        End If
        If e.KeyCode = Keys.Space Then
            blnKeyJump = False
        End If
    End Sub
    ' Handles moving the player
    Private Sub MovePlayer()
        If blnKeyRight = True Then
            Xvel += 2
        End If
        If blnKeyLeft = True Then
            Xvel -= 2
        End If
        If blnKeyJump = True Then
            Yvel = -10
        End If
        ' Gravity
        Yvel += 0.98
        ' Terminal Velocity
        If Yvel > 53 Then
            Yvel = 53
        End If
        ' Friction
        Xvel *= 0.95
        picPlayer.Left += Xvel
        picPlayer.Top += Yvel
    End Sub
    ' Handles platform colision
    Private Sub PlayerHitsPlatform()
        Dim eachPlatform As Integer
        For eachPlatform = 0 To maxPlatforms
            If picPlayer.Bounds.IntersectsWith(picPlatforms(eachPlatform).Bounds) Then
                ' Player is above platform
                picPlayer.Top = picPlatforms(eachPlatform).Top - picPlayer.Height
                Yvel = 0
            End If
        Next
    End Sub
    ' Game refresh loop
    Private Sub TmrGameLoop_Tick(sender As Object, e As EventArgs) Handles tmrGameLoop.Tick
        Call MovePlayer()
        Call PlayerHitsPlatform()
        Label1.Text = Xvel
        Label2.Text = Yvel
    End Sub
End Class