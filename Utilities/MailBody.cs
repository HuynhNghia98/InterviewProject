namespace GroupManagement.Utilities
{
	public static class MailBody
	{
		public static string RenderMailbody(string name, string userId, string token, string imageUrl)
		{
			return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Yêu cầu đổi mật khẩu</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                        }}
                        .container {{
                            background-color: #f7f7f7;
                            padding: 20px;
                            border-radius: 10px;
                        }}
                        .header {{
                            text-align: center;
                            margin-bottom: 30px;
                        }}
                        .logo {{
                            width: 100%;
                            clip: rect(0px, auto, 200px, auto);
                        }}
                        .content {{
                            font-size: 16px;
                            color: #333;
                            margin-bottom: 20px;
                        }}
                        .button {{
                            display: inline-block;
                            background-color: #e9c46a;
                            color: #ffffff;
                            padding: 15px 30px;
                            text-decoration: none;
                            border-radius: 5px;
                            margin-top: 20px;
                            font-weight: bold;
                        }}
                        .footer {{
                            margin-top: 30px;
                            font-size: 14px;
                            color: #666;
                        }}
                        .customButton {{
                            text-align: center;
                            margin-bottom: 20px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <img src='{imageUrl}' alt='Logo' class='logo'>
                            <h1>Yêu cầu đổi mật khẩu</h1>
                        </div>
                        <div class='content'>
                            <p>Xin chào {name},</p>
                            <p>Chúng tôi nhận được yêu cầu đổi mật khẩu cho tài khoản của bạn. Vui lòng sử dụng liên kết dưới đây để đặt lại mật khẩu của bạn:</p>
                                 <p>UserId: {userId}</p>
                                    <p>Token: {token}</p>
                            <p>Nếu bạn không thực hiện yêu cầu này, bạn có thể bỏ qua email này.</p>
                        </div>
                        <div class='footer'>
                            <p>Đây là email tự động, vui lòng không trả lời.</p>
                            <p>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi.</p>
                        </div>
                    </div>
                </body>
                </html>";
		}
	}
}
