import { LoginUser } from './../../services/userServices';
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { AxiosError } from 'axios';

type User = {
  token: string;
  id: number;
  username: string
  email: string;
}

type userState = {
  user: User | null;
  isLoading: boolean;
  error: string;
};

const initialState: userState = {
  user: null,
  isLoading: false,
  error: '',
};

type signIn = {
  email: string;
  password: string;
};

export const login = createAsyncThunk(
  'user/login',
  async ({ email, password }: signIn, {rejectWithValue}) => {
    try{
      const data = await LoginUser(email, password);
      return data;
    } catch(error){
      return rejectWithValue((error as AxiosError).response?.data);
    }
  }
);

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    logout: (state) => {
      state.user = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(login.pending, (state) => {
        state.isLoading = true;
        state.error = '';
      })
      .addCase(login.fulfilled, (state, action) => {
        state.isLoading = false;
        state.user = action.payload;
      })
      .addCase(login.rejected, (state, action: { payload: unknown }) => {
        state.isLoading = false;
        state.error = action.payload as string || '';
      });
  },
});

export const { logout } = userSlice.actions;
export default userSlice.reducer;

