import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

import { useAuth } from '../context/AuthContext';
import { Login } from '../services/userService';

function LoginPage() {
	const navigate = useNavigate();
	const { login } = useAuth();

	const [email, setEmail] = useState('');
	const [password, setPassword] = useState('');
	const [isSubmitting, setIsSubmitting] = useState(false);
	const [error, setError] = useState('');

	const onSubmit = async (e) => {
		e.preventDefault();
		setError('');

		if (!email.trim() || !password) {
			setError('Please enter email and password.');
			return;
		}

		setIsSubmitting(true);
		try {
			const result = await Login({ email: email.trim(), password });

			const userId =
				typeof result === 'string' || typeof result === 'number'
					? result
					: result?.userId ?? result?.id;

			if (!userId) {
				setError('Login succeeded, but no user id was returned.');
				return;
			}

			login(String(userId));
			navigate('/home');
		} catch (err) {
			const message =
				err?.response?.data?.message ||
				err?.response?.data ||
				err?.message ||
				'Login failed.';

			setError(String(message));
		} finally {
			setIsSubmitting(false);
		}
	};

	return (
		<section className="page" aria-labelledby="login-title">
			<h1 id="login-title">Login</h1>
			<form className="auth-form" onSubmit={onSubmit}>
				<label className="form-row">
					<span>Email</span>
					<input
						value={email}
						onChange={(e) => setEmail(e.target.value)}
						autoComplete="email"
						required
					/>
				</label>

				<label className="form-row">
					<span>Password</span>
					<input
						type="password"
						value={password}
						onChange={(e) => setPassword(e.target.value)}
						autoComplete="current-password"
						required
					/>
				</label>

				{error ? (
					<p role="alert" className="form-error">
						{error}
					</p>
				) : null}

				<div className="actions">
					<button type="submit" disabled={isSubmitting}>
						{isSubmitting ? 'Logging in…' : 'Login'}
					</button>
				</div>
			</form>
		</section>
	);
}

export default LoginPage;
